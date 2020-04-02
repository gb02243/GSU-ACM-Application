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
        }
        // textbox first name ENTER
        private void textBoxFirstname_Enter(object sender, EventArgs e)
        {
            String fname = firstName.Text;
            if (fname.ToLower().Trim().Equals("first name"))
            {
                firstName.Text = "";

            }
        }

        // textbox first name LEAVE
        private void textBoxFirstname_Leave(object sender, EventArgs e)
        {
            String fname = lastName.Text;
            if (fname.ToLower().Trim().Equals("first name") || fname.Trim().Equals(""))
            {
                lastName.Text = "first name";

            }
        }


        // textbox last name ENTER
        private void textBoxLastname_Enter(object sender, EventArgs e)
        {
            String lname = lastName.Text;
            if (lname.ToLower().Trim().Equals("last name"))
            {
                lastName.Text = "";

            }
        }


        // textbox last name LEAVE
        private void textBoxLastname_Leave(object sender, EventArgs e)
        {
            String lname = lastName.Text;
            if (lname.ToLower().Trim().Equals("last name") || lname.Trim().Equals(""))
            {
                lastName.Text = "last name";

            }
        }

        // textbox email ENTER
        private void textBoxEmail_Enter(object sender, EventArgs e)
        {
            String email = emailaddress.Text;
            if (email.ToLower().Trim().Equals("email address"))
            {
                emailaddress.Text = "";

            }
        }
        // textbox email LEAVE
        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            String email = emailaddress.Text;
            if (email.ToLower().Trim().Equals("email address") || email.Trim().Equals(""))
            {
                emailaddress.Text = "email address";

            }
        }
        // textbox phone number ENTER
        private void textBoxPhone_Enter(object sender, EventArgs e)
        {
            String phone = phoneNum.Text;
            if (phone.ToLower().Trim().Equals("123-456-9123"))
            {
                phoneNum.Text = "";

            }

        }

        // textbox phone number LEAVE
        private void textBoxPhone_Leave(object sender, EventArgs e)
        {
            String phone = phoneNum.Text;
            if (phone.ToLower().Trim().Equals("123-456-9123") || phone.Trim().Equals(""))
            {
                phoneNum.Text = "123-456-9123";

            }
        }
        // textbox password ENTER
        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            String pword = password.Text;
            if (pword.ToLower().Trim().Equals("password"))
            {
                password.Text = "";
                password.IsPassword = true;

            }
        }

        // textbox password LEAVE
        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            String pword = password.Text;
            if (pword.ToLower().Trim().Equals("password") || pword.Equals(""))
            {
                password.Text = "password";
                password.IsPassword = false;

            }
        }

        // textbox confirm password ENTER
        private void textBoxPasswordConfirm_Enter(object sender, EventArgs e)
        {
            String cpassword = passwordConfirm.Text;
            if (cpassword.ToLower().Trim().Equals("confirm password"))
            {
                passwordConfirm.Text = "";
                passwordConfirm.IsPassword = true;

            }
        }

        // textbox confirm password LEAVE
        private void textBoxPasswordConfirm_Leave(object sender, EventArgs e)
        {
            String cpassword = passwordConfirm.Text;
            if (cpassword.ToLower().Trim().Equals("confirm password") ||
                cpassword.ToLower().Trim().Equals("password") ||
                cpassword.Trim().Equals(""))
            {
                passwordConfirm.Text = "confirm password";
                passwordConfirm.IsPassword = false;

            }
        }

        // button signup
        private void buttonCreateAccount_Click(object sender, EventArgs e)
        {
            // add a new user
            DataTable table = new DataTable();
            DB db = new DB();
            if (db.openConnection() == false)
            {
                DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                db.closeConnection();
                String fname = firstName.Text;
                MySqlCommand command = new MySqlCommand("INSERT INTO user(fname,lname,phone,email,password) VALUES (@fn, @ln,@ph,@email, @pass)", db.getConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = firstName.Text;
                command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lastName.Text;
                command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phoneNum.Text;
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailaddress.Text;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password.Text;
                MySqlCommand command2 = new MySqlCommand("SELECT * FROM user WHERE email = @email2 and password = @pass2", db.getConnection());
                command2.Parameters.Add("@email2", MySqlDbType.VarChar).Value = emailaddress.Text;
                command2.Parameters.Add("@pass2", MySqlDbType.VarChar).Value = password.Text;
                // open the connection
                db.openConnection();
              
               
                adapter.SelectCommand = command2;
                adapter.Fill(table);
                int y = table.Rows.Count;
                if (firstName == null)
                {
                    DisplayAlert("Server Error", "Try Again Later", "Ok");
                }
                Console.WriteLine(String.IsNullOrEmpty(firstName.Text));


                //password.Text.Equals(passwordConfirm.Text) && phoneNum.Text.ValidatePhoneNumber(true) && emailaddress.Text.IsValidEmail() && password.Text.IsValidPassword()
                // check if the textboxes contains the default values 
                if (checkTextBoxesValues() == true && y == 0)
                {
                    // check if the password equal the confirm password
                    if (password.Text.Equals(passwordConfirm.Text) && phoneNum.Text.ValidatePhoneNumber(true) && emailaddress.Text.IsValidEmail() && password.Text.IsValidPassword())
                    {





                        // execute the query
                        if (command.ExecuteNonQuery() == 1)
                        {
                            DisplayAlert("Your Account Has Been Created", "Account Created", "Ok");
                            labelGoToLogin_Click(sender, e);
                        }
                        else
                        {
                            DisplayAlert("ERROR", "Account Failed to Create", "Ok");
                        }

                    }
                    else
                    {
                        if (password.Text.Equals(passwordConfirm.Text) == false)
                        {
                            DisplayAlert("Confirm password doesnt match", "Password Error", "Ok");
                        }
                        else if (phoneNum.Text.ValidatePhoneNumber(true) == false)
                        {
                            DisplayAlert("Invalid phone number", "phone Number Error", "Ok");
                        }
                        else if (emailaddress.Text.IsValidEmail() == false)
                        {
                            DisplayAlert("Invalid Email", "Email", "Ok");
                        }
                        else
                        {
                            DisplayAlert("Password must contain between 8-16 characters", "Password error", "Ok");
                        }
                    }
                }
                else
                {
                    DisplayAlert("Enter Your Information First Or Email Already Used", "Error", "Ok");
                }



                // close the connection
                db.closeConnection();
            }
            db.closeConnection();
        }




        // check if the textboxes contains the default values
        public Boolean checkTextBoxesValues()
        {
            
            if (String.IsNullOrEmpty(firstName.Text))
            {
                return false;
            }
               if (String.IsNullOrEmpty(lastName.Text))
            {
                return false;
            }
            if (String.IsNullOrEmpty(emailaddress.Text))
            {
                return false;
            }
            if (String.IsNullOrEmpty(phoneNum.Text))
            {
                return false;
            }
            if (String.IsNullOrEmpty(password.Text))
            {
                return false;
            }
          
            else
            {
                return true;
            }
            
        }

        // label go to the login form CLICK
        private async void labelGoToLogin_Click(object sender, EventArgs e)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            await Navigation.PushAsync(new LoginPage());
        }

    }
}

