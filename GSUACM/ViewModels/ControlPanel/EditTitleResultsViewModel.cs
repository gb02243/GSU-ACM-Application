using GSUACM.Models;
using GSUACM.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ControlPanel
{
    class EditTitleResultsViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public User SelectedUser { get; set; }
        public string PageTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand SaveUserCommand { get; set; }
        public bool UserIsAdmin { get; set; }
        public string NewTitle { get; set; }
        public string ProfileImage { get; set; }
        public EditTitleResultsViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            CloseWindowCommand = new Command(CloseWindow);
            SaveUserCommand = new Command(SaveUser);
            SelectedUser = GlobalVars.SelectedUser;
            PageTitle = SelectedUser.fullName;
            FirstName = SelectedUser.fname;
            LastName = SelectedUser.lname;
            Title = SelectedUser.title;
            UserIsAdmin = SelectedUser.isAdmin;
            ProfileImage = GlobalVars.SelectedUser.ProfileImage;
        }

        private async void SaveUser()
        {
            DB db = new DB();
            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                // create the adapter and query
                MySqlCommand command = new MySqlCommand("UPDATE user SET isAdmin = "+UserIsAdmin+", title = (\'"+NewTitle+"\') WHERE userID LIKE(\"" + SelectedUser.userID + "\")", db.getConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                //Console.WriteLine("Command Created");
                db.openConnection();
                adapter.SelectCommand = command;

                if (command.ExecuteNonQuery() == 1)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    await Application.Current.MainPage.DisplayAlert("Thank you", "User Updated", "Ok");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
                db.closeConnection();
            }
            db.closeConnection();
            MessagingCenter.Send(this, "title");
        }

        private async void CloseWindow(object obj)
        {
            GlobalVars.SelectedUser = null;
            await Navigation.PopModalAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
