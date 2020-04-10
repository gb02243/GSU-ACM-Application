﻿using System;
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
        public static User User { get; set; }
        public App()
        {
            InitializeComponent();

            //App.Current.MainPage = new AppShell();
            MainPage = new Views.Polls.PollsPage();
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

        //TODO: retrieve all user info
        public static void InstantiateUser(string fname, string lname, string userID)
        {
            User = new User
            {
                fname = fname,
                lname = lname,
                userID = userID
            };
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
