using System;
using System.Collections.Generic;
using System.Data;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
using GSUACM.ViewModels;
using System.Diagnostics;
using System.Data.SqlClient;

namespace GSUACM.Views
{
    public partial class editProfile : ContentPage
    {
        public editProfile()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel(this.Navigation);
        }
     
        private void buttonUpdateProfile_Click(Object sender, EventArgs e)
        {

        }

        private void changePasswowrd_Click(Object sender, EventArgs e)
        {

        }
        private void displayEmailChecked(Object sender, EventArgs e)
        {

        }
        
        private void displayPhoneChecked(Object sender, EventArgs e)
        {

        }
    }
}
