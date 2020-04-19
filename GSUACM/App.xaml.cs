using GSUACM.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSUACM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //GlobalVars.InstantiateUser("Griffin", "Bryant", "1", "Administrator", "true");
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
