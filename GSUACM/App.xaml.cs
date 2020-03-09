using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GSUACM.Services;
using GSUACM.Views;

namespace GSUACM
{
    public partial class App : Application
    {
        public bool isLoggedIn;
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            //isLoggedIn = false;

            //if (isLoggedIn)
            //{
            //    MainPage = new MainPage();
            //}
            //else
            //{
            //    MainPage = new WelcomePage();
            //}

            MainPage = new SignupPage();

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
