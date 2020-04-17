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
        //tables for database columns
        private DataTable table = new DataTable();

        public ICommand cancel { get; }
        public INavigation Navigation { get; set; }
        public ICommand editPage { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String ClubPoints { get; set; }
        public String fname { get; set; }
        public String email { get; set; }
        public String number { get; set; }
        public String lname { get; set; }
        public ICommand buttonUpdateProfile_Click { get; set; }



        public ProfileViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.cancel = new Command(this.ReturnToProfile);
            this.editPage = new Command(this.edit);
            this.buttonUpdateProfile_Click = new Command(this.updateProfile);
            getDBconnection();
            getEmail();
            getName();
            getAddress();
            getClubPoints();

        }

        //editPage -> profile page
        private async void ReturnToProfile()
        {
            await Navigation.PopModalAsync();

        }

        //Profile page -> Editpage
        private async void edit()
        {
            await this.Navigation.PushModalAsync(new editProfile());
        }

        private async void updateProfile()
        {
            DB db = new DB();
            //Console.WriteLine("Server"+db.openConnection());
            if (db.openConnection() == false)
            {
                db.closeConnection();
             await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                String firstName = fname;
                String lastName = lname;
                String emailAddition = email;
                String num = number;

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
            }
            

            

            //updates User
            GlobalVars.InstantiateUser(table.Rows[0]["fname"].ToString(), table.Rows[0]["lname"].ToString(), table.Rows[0]["userID"].ToString(), table.Rows[0]["title"].ToString());

            

            GlobalVars.User.points = table.Rows[0]["points"].ToString();
            GlobalVars.User.email = table.Rows[0]["email"].ToString();
            GlobalVars.User.phone = table.Rows[0]["phone"].ToString();

            db.closeConnection();
            await Application.Current.MainPage.DisplayAlert("Your is Account Updated", "Account Updated", "Ok");
            await Navigation.PopModalAsync();

            

        }

        private void getName()
        {
            GlobalVars.User.fname = this.Name;
        }

        private void getEmail()
        {
            GlobalVars.User.email = this.Email;
        }

        private void getAddress()
        {
            GlobalVars.User.phone = this.Address;
        }
        private void getClubPoints()
        {
            GlobalVars.User.points = this.ClubPoints;
        }

        private void getDBconnection()
        {
            DB db = new DB();
            //Console.WriteLine("Server"+db.openConnection());
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
                MySqlCommand command = new MySqlCommand("SELECT * FROM user", db.getConnection());
                //command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                //command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
                db.openConnection();
                adapter.SelectCommand = command;

                adapter.Fill(table);
            }
            db.closeConnection();


            //updates User
              //GlobalVars.InstantiateUser(table.Rows[0]["fname"].ToString(), table.Rows[0]["lname"].ToString(), table.Rows[0]["userID"].ToString(), table.Rows[0]["title"].ToString());

            GlobalVars.User.points = table.Rows[0]["points"].ToString();
            GlobalVars.User.email = table.Rows[0]["email"].ToString();
            GlobalVars.User.phone = table.Rows[0]["phone"].ToString();

            OnPropertyChanged();

        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }






        public event PropertyChangedEventHandler PropertyChanged;
    }
    
}

