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
        

        public ProfileViewModel(INavigation navigation)
        {
            
            this.editPage = new Command(this.edit);
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

        private void getName()
        {
            Name = GlobalVars.User.fullName;
        }

        private void getEmail()
        {
            Email = GlobalVars.User.email;
        }

        private void getAddress()
        {
            Address = GlobalVars.User.phone;
        }
        private void getClubPoints()
        {
            ClubPoints = GlobalVars.User.points;
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
            //  GlobalVars.InstantiateUser(table.Rows[0]["fname"].ToString(), table.Rows[0]["lname"].ToString(), table.Rows[0]["userID"].ToString(), table.Rows[0]["title"].ToString());

            GlobalVars.User.points = table.Rows[0]["points"].ToString();
            GlobalVars.User.email = table.Rows[0]["email"].ToString();
            GlobalVars.User.phone = table.Rows[0]["phone"].ToString();

        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
    
}

