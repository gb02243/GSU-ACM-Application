using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GSUACM.Services;
using GSUACM.Views;
using GSUACM.Models;

namespace GSUACM
{
    public partial class App : Application
    {
        public static User User { get; set; }
        public App()
        {
            InitializeComponent();

            if (User != null)
                MainPage = new AppShell();
            else
                MainPage = new LoginPage();

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
