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
            LoginPage log = new LoginPage();
            MainPage = new NavigationPage(log);
            NavigationPage.SetHasBackButton(log, false);

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
