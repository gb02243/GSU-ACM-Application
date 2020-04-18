using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Input;
using GSUACM.ViewModels;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using System.Diagnostics;
using System.Data;
using GSUACM.Services;
using GSUACM;
using GSUACM.Views;
using System.Runtime.CompilerServices;

namespace GSUACM.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //tables for database columns
        private DataTable table = new DataTable();

        public ICommand cancel { get; }
        public INavigation Navigation { get; set; }
        public ICommand editPage { get; set; }
        public ICommand changePassword { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String ClubPoints { get; set; }
        public String Number { get; set; }
        

        public ProfileViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.cancel = new Command(this.goBack);
            this.changePassword = new Command(this.changePasswordGo);
            this.editPage = new Command(this.editPageGo);



            getDBconnection();
            this.Name = GlobalVars.User.fullName;
            this.Number = GlobalVars.User.phone;
            this.Email = GlobalVars.User.email;
            this.ClubPoints = GlobalVars.User.points;

           

            //updates page when message recieved
            MessagingCenter.Subscribe<editProfileViewModel>(this, "update", (sender) =>
            {
                this.Name = GlobalVars.User.fullName;
                this.Number = GlobalVars.User.phone;
                this.Email = GlobalVars.User.email;
                this.ClubPoints = GlobalVars.User.points;

                OnPropertyChanged();
            });
        }

        private async void editPageGo()
        {
            await this.Navigation.PushModalAsync(new editProfile());
        }

        private async void changePasswordGo()
        {
            await this.Navigation.PushModalAsync(new ChangedPassword());
        }

        private async void goBack()
        {
            await Navigation.PopModalAsync();
        }

        private void getDBconnection()
        {
            DB db = new DB();

            if (db.openConnection() == false)
            {
                db.closeConnection();
                Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                String email = GlobalVars.User.email;
                String password = GlobalVars.User.password;

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM user where userID = @userID", db.getConnection());
                command.Parameters.Add("@userID", MySqlDbType.VarChar).Value = GlobalVars.User.userID;

                db.openConnection();
                adapter.SelectCommand = command;

                adapter.Fill(table);
            }
            db.closeConnection();
            //checks if table is empty
            if (table.Rows.Count > 0)
            {
                GlobalVars.InstantiateUser(table.Rows[0]["fname"].ToString(), table.Rows[0]["lname"].ToString(), table.Rows[0]["userID"].ToString(), table.Rows[0]["title"].ToString());
                GlobalVars.User.points = table.Rows[0]["points"].ToString();
                GlobalVars.User.email = table.Rows[0]["email"].ToString();
                GlobalVars.User.phone = table.Rows[0]["phone"].ToString();

                this.Name = GlobalVars.User.fullName;
                this.Number = GlobalVars.User.phone;
                this.Email = GlobalVars.User.email;
                this.ClubPoints = GlobalVars.User.points;

            }

            else
            {
                Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
    }
    
}

