using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GSUACM.Services;
using GSUACM.Views;

namespace GSUACM
{
    public partial class App : Application
    {
        public static bool isLoggedIn { get; set; }
        public static bool isAdmin { get; set; }

        //TODO: remember to fix this
        public static string User { get; set; }

        public App()
        {
            InitializeComponent();
            // TODO: remove these
            isLoggedIn = true;
            isAdmin = true;
            User = "Griffin";

            if (isLoggedIn)
                MainPage = new AppShell();
            else
                MainPage = new LoginPage();

            //MainPage = new Controls.Cards.CardViewUI();

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
