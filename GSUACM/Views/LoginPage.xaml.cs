using System;
using System.Collections.Generic;
using System.Data;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
using GSUACM.ViewModels;
using System.Diagnostics;
using System.Data.SqlClient;
using XF_Login.ViewModels;

namespace GSUACM.Views
{
    public partial class LoginPage : ContentPage
    {
        private DataTable table = new DataTable();
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(this.Navigation);
        }
    }
}

