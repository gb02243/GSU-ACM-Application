using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
using GSUACM.ViewModels;
using Xamarin.Forms.Xaml;

namespace GSUACM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        // button login
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            DB db = new DB();
            if (db.openConnection() == false)
            {

                DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            db.closeConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            String fname = name.Text;
            String emailAddress = email.Text;
            String phoneNum = phone.Text;
            MySqlCommand command = new MySqlCommand("SELECT fname,lname,email,phone FROM user WHERE ", db.getConnection());
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email.Text;

            db.openConnection();
            adapter.SelectCommand = command;

            adapter.Fill(table);
        }
        private async void changePassword_Click(object sender, EventArgs e)
        {

        }
        private async void buttonCreateProfile_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new createProfile());
        }
        private async void buttonEditProfile_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new editProfile());
        }
        // label go to signup CLICK
        private async void labelGoToSignUp_Click(object sender, EventArgs e)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            await Navigation.PushAsync(new SignupPage());
        }
        // label go to signup CLICK
        private async void labelLogout_Click(object sender, EventArgs e)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            await Navigation.PushAsync(new LoginPage());
        }
        //set text boxes up
        public void setUpHomePage(string fname, string lname, string emailaddress, string phoneNum, String points)
        {
            name.Text = fname + " " + lname;
            email.Text = emailaddress;
            phone.Text = phoneNum;
            if (points == null)
            {
                point.Text = "0";
            }
            else
            {
                point.Text = points;
            }
        }

    }
}