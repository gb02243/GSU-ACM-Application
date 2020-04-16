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


        public editProfileViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.cancel = new Command(this.ReturnToProfile);

        }
        //editPage -> profile page
        private async void ReturnToProfile()
        {
            await Navigation.PopModalAsync();

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

