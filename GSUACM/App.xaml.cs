using GSUACM.Services;
using GSUACM.Models;
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
            if (Application.Current.Properties.ContainsKey("UserID"))
            {
                GlobalVars.InstantiateUser(Application.Current.Properties["UserFName"].ToString(), 
                    Application.Current.Properties["UserLName"].ToString(), 
                    Application.Current.Properties["UserID"].ToString(), 
                    Application.Current.Properties["UserTitle"].ToString(), 
                    Application.Current.Properties["UserIsAdmin"].ToString(),
                    Application.Current.Properties["UserIsTutor"].ToString(),
                    Application.Current.Properties["UserEmail"].ToString(),
                    Application.Current.Properties["UserPhone"].ToString(),
                    Application.Current.Properties["UserClubPoints"].ToString(),
                    Application.Current.Properties["UserImage"].ToString());
                
            }
            else
            {
                App.Current.MainPage = new AppShell();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            Application.Current.SavePropertiesAsync();
        }

        protected override void OnResume()
        {
        }
    }
}
