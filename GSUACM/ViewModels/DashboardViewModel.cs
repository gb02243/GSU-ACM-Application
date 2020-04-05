using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace GSUACM.ViewModels
{
    class DashboardViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public string User { get; set; }
        public string WelcomeMessage => "Welcome, "+User+"!";
       
        public DashboardViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            User = App.User;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
