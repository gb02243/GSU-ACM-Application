using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GSUACM.Services;
using GSUACM.Views;
using GSUACM.Models;
using GSUACM.Views.Dashboard;

namespace GSUACM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //App.Current.MainPage = new AppShell();
            App.Current.MainPage = new NavigationPage(new Views.Control_Panel.ControlPanelPage());
            GlobalVars.InstantiateUser("Griffin", "Bryant", "1", "Administrator");
            //TODO: implement
            //if (User != null)
            //{
            //    App.Current.MainPage = new NavigationPage(new LoginPage());
            //}
            //else
            //{
            //    App.Current.MainPage = new NavigationPage(new DashboardPage());
            //}
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
