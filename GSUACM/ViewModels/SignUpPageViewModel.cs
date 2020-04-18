using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using GSUACM.Views;

namespace GSUACM.ViewModels
{
    class SignUpPageViewModel
    {
        public string firstName { get; set; }
       
        public string lastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string PhoneNum { get; set; }
        public INavigation Navigation { get; set; }
        public ICommand SignupCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        
        // textbox first name ENTER
        public SignUpPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.SignupCommand = new Command(this.buttonCreateAccount_Click);
            this.CancelCommand = new Command(this.buttonCancel_Click);
        }

        private void buttonCancel_Click(object obj)
        {
            Navigation.PopModalAsync();
        }

        private void textBoxFirstname_Enter()
        {
            String fname = firstName;
            if (fname.ToLower().Trim().Equals("first name"))
            {
                firstName= "";

            }
        }

        // textbox first name LEAVE
        private void textBoxFirstname_Leave()
        {
            String fname = lastName;
            if (fname.ToLower().Trim().Equals("first name") || fname.Trim().Equals(""))
            {
                lastName = "first name";

            }
        }


        // textbox last name ENTER
        private void textBoxLastname_Enter()
        {
            String lname = lastName;
            if (lname.ToLower().Trim().Equals("last name"))
            {
                lastName = "";

            }
        }


        // textbox last name LEAVE
        private void textBoxLastname_Leave()
        {
            String lname = lastName;
            if (lname.ToLower().Trim().Equals("last name") || lname.Trim().Equals(""))
            {
                lastName = "last name";

            }
        }

        // textbox email ENTER
        private void textBoxEmail_Enter()
        {
            String email = Email;
            if (email.ToLower().Trim().Equals("email address"))
            {
                Email = "";

            }
        }
        // textbox email LEAVE
        private void textBoxEmail_Leave()
        {
            String email = Email;
            if (email.ToLower().Trim().Equals("email address") || email.Trim().Equals(""))
            {
                Email = "email address";

            }
        }
        // textbox phone number ENTER
        private void textBoxPhone_Enter()
        {
            String phone = PhoneNum;
            if (phone.ToLower().Trim().Equals("123-456-9123"))
            {
                PhoneNum = "";

            }

        }

        // textbox phone number LEAVE
        private void textBoxPhone_Leave()
        {
            String phone = PhoneNum;
            if (phone.ToLower().Trim().Equals("123-456-9123") || phone.Trim().Equals(""))
            {
                PhoneNum = "123-456-9123";

            }
        }
        // textbox password ENTER
        private void textBoxPassword_Enter()
        {
            String pword = Password;
            if (pword.ToLower().Trim().Equals("password"))
            {
                Password = "";
                //password.IsPassword = true;

            }
        }

        // textbox password LEAVE
        private void textBoxPassword_Leave()
        {
            String pword = Password;
            if (pword.ToLower().Trim().Equals("password") || pword.Equals(""))
            {
                Password = "password";
               // Password.IsPassword = false;

            }
        }

        // textbox confirm password ENTER
        private void textBoxPasswordConfirm_Enter()
        {
            String cpassword = PasswordConfirm;
            if (cpassword.ToLower().Trim().Equals("confirm password"))
            {
                PasswordConfirm = "";
                //PasswordConfirm.IsPassword = true;

            }
        }

        // textbox confirm password LEAVE
        private void textBoxPasswordConfirm_Leave(object sender, EventArgs e)
        {
            String cpassword = PasswordConfirm;
            if (cpassword.ToLower().Trim().Equals("confirm password") ||
                cpassword.ToLower().Trim().Equals("password") ||
                cpassword.Trim().Equals(""))
            {
                PasswordConfirm = "confirm password";
                //passwordConfirm.IsPassword = false;

            }
        }

        // button signup
        private void buttonCreateAccount_Click()
        {
            // add a new user
            DataTable table = new DataTable();
            DB db = new DB();
            if (db.openConnection() == false)
            {
                Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                db.closeConnection();
                String fname = firstName;
                //check sql
                MySqlCommand command = new MySqlCommand("INSERT INTO user(fname,lname,phone,email,password,title,userID) VALUES (@fn, @ln, @ph, @email, @pass, 'Member',@userID)", db.getConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = firstName;
                command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lastName;
                command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = PhoneNum;
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = Email;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = Password;
                command.Parameters.Add("@userID", MySqlDbType.VarChar).Value = Guid.NewGuid().ToString();
                MySqlCommand command2 = new MySqlCommand("SELECT * FROM user WHERE email = @email2 and password = @pass2", db.getConnection());
                command2.Parameters.Add("@email2", MySqlDbType.VarChar).Value = Email;
                command2.Parameters.Add("@pass2", MySqlDbType.VarChar).Value = Password;
                // open the connection
                db.openConnection();


                adapter.SelectCommand = command2;
                adapter.Fill(table);
                int y = table.Rows.Count;
  
                //Console.WriteLine(String.IsNullOrEmpty(firstName));


                //password.Text.Equals(passwordConfirm.Text) && phoneNum.Text.ValidatePhoneNumber(true) && emailaddress.Text.IsValidEmail() && password.Text.IsValidPassword()
                // check if the textboxes contains the default values 
                if (checkTextBoxesValues() == true && y == 0)
                {
                    // check if the password equal the confirm password
                    if (Password.Equals(PasswordConfirm) && PhoneNum.ValidatePhoneNumber(true) && Email.IsValidEmail() && Password.IsValidPassword())
                    {





                        // execute the query
                        if (command.ExecuteNonQuery() == 1)
                        {
                            Application.Current.MainPage.DisplayAlert("Your Account Has Been Created", "Account Created", "Ok");
                            Navigation.PopModalAsync();
                        }
                        else
                        {
                            Application.Current.MainPage.DisplayAlert("ERROR", "Account Failed to Create", "Ok");
                        }

                    }
                    else
                    {
                        if (Password.Equals(PasswordConfirm) == false)
                        {
                            Application.Current.MainPage.DisplayAlert("Confirm password doesnt match", "Password Error", "Ok");
                        }
                        else if (PhoneNum.ValidatePhoneNumber(true) == false)
                        {
                            Application.Current.MainPage.DisplayAlert("Invalid phone number", "phone Number Error", "Ok");
                        }
                        else if (Email.IsValidEmail() == false)
                        {
                            Application.Current.MainPage.DisplayAlert("Invalid Email", "Email", "Ok");
                        }
                        else
                        {
                            Application.Current.MainPage.DisplayAlert("Password must contain between 8-16 characters", "Password error", "Ok");
                        }
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Enter Your Information First Or Email Already Used", "Error", "Ok");
                }



                // close the connection
                db.closeConnection();
            }
            db.closeConnection();
        }




        // check if the textboxes contains the default values
        public Boolean checkTextBoxesValues()
        {

            if (String.IsNullOrEmpty(firstName))
            {
                return false;
            }
            if (String.IsNullOrEmpty(lastName))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Email))
            {
                return false;
            }
            if (String.IsNullOrEmpty(PhoneNum))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
