using GSUACM.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Xamarin.Forms;
namespace GSUACM.Views
{
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
            BindingContext = new SignUpPageViewModel(this.Navigation);
        }
    }
}

