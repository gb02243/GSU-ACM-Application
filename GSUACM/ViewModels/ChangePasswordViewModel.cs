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
    public class ChangePasswordViewModel : INotifyPropertyChanged
    {
        //tables for database columns
        private DataTable table = new DataTable();

        public string currentPassword { get; set; }
        public string newPassword { get; set; }
        public string newPasswordConfirm { get; set; }
        public ICommand cancel { get; set; }
        public ICommand changePassword { get; set; }
        public INavigation Navigation { get; set; }

        
        public ChangePasswordViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.cancel = new Command(this.ReturnToProfile);
            this.changePassword = new Command(this.getDBconnection);
            updatedPassword();
        }

        private void updatedPassword()
        {
            GlobalVars.User.password = this.newPassword;
        }

        private async void ReturnToProfile()
        {
            await Navigation.PopModalAsync();

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
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

