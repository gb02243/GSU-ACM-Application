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
        public ICommand cancel { get; }
        public INavigation Navigation { get; set; }
        public ICommand editPage { get; set; }

        public ProfileViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.cancel = new Command(this.ReturnToProfile);
            this.editPage = new Command(this.edit);
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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

