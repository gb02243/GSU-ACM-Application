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
namespace GSUACM.ViewModels
{
    public class editProfileViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand cancel { get; }
        public ICommand buttonUpdateProfile_Click { get; set; }
        public ICommand changePasswowrd_Click { get; set; }
        public String Email { get; set; }
        public String fname { get; set; }
        public String Number { get; set; }
        public String lname { get; set; }

        //tables for database columns
        private DataTable table = new DataTable();

        public editProfileViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.changePasswowrd_Click = new Command(this.changePasswordGo);
            this.buttonUpdateProfile_Click = new Command(this.updateProfile);
            this.cancel = new Command(this.ReturnToProfile);
        }

        //Profile page -> Editpage
        private async void edit()
        {
            await this.Navigation.PushModalAsync(new editProfile());
        }
        //Editpage -> changePassword
        private async void changePasswordGo()
        {
            await this.Navigation.PushModalAsync(new GSUACM.Views.ChangePassword());
        }

        //editPage -> profile page
        private async void ReturnToProfile()
        {
            await Navigation.PopModalAsync();

        }

        private async void updateProfile()
        {
            DB db = new DB();

            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                String firstName = fname;
                String lastName = lname;
                String emailAddition = Email;
                String num = Number;

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("update user set email = @email,phone = @phone,fname = @fname,lname = @lname where userID = @userid", db.getConnection());
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailAddition;
                command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = num;
                command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = firstName;
                command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lastName;
                command.Parameters.Add("@userid", MySqlDbType.VarChar).Value = GlobalVars.User.userID;
                db.openConnection();
                adapter.SelectCommand = command;


                adapter.Fill(table);


                //updates User
                GlobalVars.InstantiateUser(table.Rows[0]["fname"].ToString(), table.Rows[0]["lname"].ToString(), table.Rows[0]["userID"].ToString(), table.Rows[0]["title"].ToString());
                GlobalVars.User.points = table.Rows[0]["points"].ToString();
                GlobalVars.User.email = table.Rows[0]["email"].ToString();
                GlobalVars.User.phone = table.Rows[0]["phone"].ToString();



                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Your is Account Updated", "Account Updated", "Ok");

                MessagingCenter.Send<editProfileViewModel>(this, "update");
                await Navigation.PopModalAsync();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

