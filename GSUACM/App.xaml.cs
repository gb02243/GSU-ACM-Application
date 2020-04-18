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

            GlobalVars.InstantiateUser("Griffin","Bryant","1","Administrator","true");
            
            App.Current.MainPage = new AppShell();
            //App.Current.MainPage = new NavigationPage(new ControlPanelPage());
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
