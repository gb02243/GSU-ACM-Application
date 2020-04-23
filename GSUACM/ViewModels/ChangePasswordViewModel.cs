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
        public event PropertyChangedEventHandler PropertyChanged;
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

        }

        private async void ReturnToProfile()
        {
            await Navigation.PopModalAsync();

        }

        private async void getDBconnection()
        {
            DB db = new DB();

            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                String oldPassWord = currentPassword;
                String newPass1 = newPassword;
                String newPass2 = newPasswordConfirm;
                //checks if passwords match
                if (String.IsNullOrWhiteSpace(oldPassWord) == true || String.IsNullOrWhiteSpace(newPass1) == true || String.IsNullOrWhiteSpace(newPass2) == true)
                {
                    await Application.Current.MainPage.DisplayAlert("Null Attributes", "Empty Spaces", "Ok");
                    ReturnToProfile();
                }
                else if (string.Equals(newPass1, newPass2) == false)
                {
                    await Application.Current.MainPage.DisplayAlert("Incorrect Password", "Ensure both passwords match & your old password is correct", "Ok");
                    ReturnToProfile();
                }
                else if (newPass1.Length < 8 || newPass2.Length < 8)
                {
                    await Application.Current.MainPage.DisplayAlert("Password Length", "Ensure both new passwords are at least 8 characters long", "Ok");
                    ReturnToProfile();
                }
               
                else
                {
                    //database checks if oldpassword matches whats is stored
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("update user set password = @newPassword where userID = @userid and password = @password", db.getConnection());
                    command.Parameters.Add("@newPassword", MySqlDbType.VarChar).Value = newPassword;
                    command.Parameters.Add("@password", MySqlDbType.VarChar).Value = currentPassword;
                    command.Parameters.Add("@userid", MySqlDbType.VarChar).Value = GlobalVars.User.userID;
                    db.openConnection();
                    adapter.SelectCommand = command;


                    command.ExecuteNonQuery();
                    GlobalVars.User.password = newPassword;


                    db.closeConnection();


                    await Application.Current.MainPage.DisplayAlert("Your Password is Updated", "Password Updated", "Ok");
                    await Navigation.PopModalAsync();
                }
            }
        }

    }
}

