using GSUACM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels
{
    class HomePageViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ClubPoints { get; set; }
        public INavigation Navigation { get; set; }
        public ICommand CreateProfileCommand { get; set; }
        public ICommand EditProfileCommand { get; set; }
        public ICommand BecomeTutorCommand { get; set; }
        

        public HomePageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            MessagingCenter.Subscribe<LoginViewModel, string>(this, "Hi", (sender, arg) => {
                Name = Services.GlobalVars.fname + " " + Services.GlobalVars.lname;
                Email = Services.GlobalVars.email;
                Phone = Services.GlobalVars.phone;
                ClubPoints = Services.GlobalVars.clubpoints;
            });
            MessagingCenter.Subscribe<EditProfileViewModel, string>(this, "Hi", (sender, arg) => {
                Name = Services.GlobalVars.fname + " " + Services.GlobalVars.lname;
                Email = Services.GlobalVars.email;
                Phone = Services.GlobalVars.phone;
                ClubPoints = Services.GlobalVars.clubpoints;
            });
            Name = Services.GlobalVars.fname +" "+ Services.GlobalVars.lname;
            Email = Services.GlobalVars.email;
            Phone = Services.GlobalVars.phone;
            ClubPoints = Services.GlobalVars.clubpoints;
            this.CreateProfileCommand = new Command(this.buttonCreateProfile_Click);
            this.EditProfileCommand = new Command(this.buttonEditProfile_Click);
            this.BecomeTutorCommand = new Command(this.buttonBecomeTutor_Click);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private async void buttonCreateProfile_Click()
        {
            await Navigation.PushAsync(new createProfile());
        }
        private async void buttonEditProfile_Click()
        {
            await Navigation.PushModalAsync(new editProfile());
        }
        private async void buttonBecomeTutor_Click()
        {
            await Navigation.PushModalAsync(new BecomeTutor());
        }



    }
}

