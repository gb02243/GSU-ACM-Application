using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GSUACM.Services;
using GSUACM.Views;
using GSUACM.Models;
using GSUACM.Views.Dashboard;
using GSUACM.Views.Control_Panel;

namespace GSUACM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();            
            App.Current.MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
