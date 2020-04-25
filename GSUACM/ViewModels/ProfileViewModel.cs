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
using GSUACM.Views.Profile;

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
        public ImageSource someImage { get; set; }


        public ProfileViewModel(INavigation navigation)
        {
           
            this.Navigation = navigation;
            this.cancel = new Command(this.goBack);
            this.changePassword = new Command(this.changePasswordGo);
            this.editPage = new Command(this.editPageGo);

            this.Name = GlobalVars.User.fullName;
            this.Number = GlobalVars.User.phone;
            this.Email = GlobalVars.User.email;
            this.ClubPoints = GlobalVars.User.ClubPoints;
            
            this.someImage = GlobalVars.User.ProfileImage;
Console.WriteLine(someImage.ToString());

            //updates page when message recieved
            MessagingCenter.Subscribe<EditProfileViewModel>(this, "update", (sender) =>
            {
                this.Name = GlobalVars.User.fullName;
                this.Number = GlobalVars.User.phone;
                this.Email = GlobalVars.User.email;
                this.ClubPoints = GlobalVars.User.ClubPoints;

                OnPropertyChanged();
            });
            MessagingCenter.Subscribe<pickPictureViewModel>(this, "update", (sender) =>
            {
                this.someImage = GlobalVars.User.ProfileImage;
                OnPropertyChanged();
            });
        }

        private async void editPageGo()
        {
            await this.Navigation.PushModalAsync(new editProfile());
        }

        private async void changePasswordGo()
        {
            await this.Navigation.PushModalAsync(new ChangePassword());
        }

        private async void goBack()
        {
            await Navigation.PopModalAsync();
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

