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
    public partial class LoginPage : ContentPage
    {
        private DataTable table = new DataTable();
        public LoginPage()
        {
            InitializeComponent();
        }



        // label close CLICK
        private void labelClose_Click(object sender, EventArgs e)
        {
            //this.Close();
            //Application.Exit();
        }

        // button login
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            //Console.WriteLine("Server"+db.openConnection());
            if (db.openConnection() == false)
            {
                db.closeConnection();
                DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                String emailAddress = email.Text;
                String pword = password.Text;
                MySqlCommand command = new MySqlCommand("SELECT * FROM user WHERE email = @email and password = @pass", db.getConnection());
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email.Text;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password.Text;
                db.openConnection();
                adapter.SelectCommand = command;

                adapter.Fill(table);



                //check if the user exists or not
                if (table.Rows.Count > 0)
                {
                    App.InstantiateUser(table.Rows[0]["fname"].ToString(), table.Rows[0]["lname"].ToString(), table.Rows[0]["userID"].ToString());
                    db.closeConnection();
                    labelGoToHomePage_Click(sender, e);
                }
                else
                {
                    // check if the username field is empty
                    if (emailAddress == null || emailAddress == "")
                    {
                        DisplayAlert("Enter Your Username To Login", "Empty Username", "Ok");
                    }
                    // check if the password field is empty
                    else if (pword == null || pword == "")
                    {
                        DisplayAlert("Enter Your Password To Login", "Empty Password", "Ok");
                    }

                    // check if the username or the password don't exist
                    else
                    {
                        Console.WriteLine(command);
                        DisplayAlert("Wrong Username Or Password", "Wrong Data", "Ok");
                    }

                }
                db.closeConnection();
            }
            db.closeConnection();
        }
        // label go to signup CLICK
        private async void labelGoToSignUp_Click(object sender, EventArgs e)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            await Navigation.PushAsync(new SignupPage());
        }
        // label go to homepage CLICK
        private async void labelGoToHomePage_Click(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            //TODO: event handling to update dashboard page
        }
        // get the first name CLICK
        private String getFirstName(object sender, EventArgs e)
        {
            string fname = table.Rows[0]["fname"].ToString();
            return fname;
        }
        // get the last name CLICK
        private String getLastName(object sender, EventArgs e)
        {
            string lname = table.Rows[0]["lname"].ToString();
            return lname;
        }
        // get the email CLICK
        private String getEmail(object sender, EventArgs e)
        {
            string email = table.Rows[0]["email"].ToString();
            return email;
        }
        // get the email CLICK
        private String getPhone(object sender, EventArgs e)
        {
            string phone = table.Rows[0]["phone"].ToString();
            return phone;
        }
        private String getPoints(object sender, EventArgs e)
        {
            string points = table.Rows[0]["points"].ToString();
            return points;
        }
    }
}

