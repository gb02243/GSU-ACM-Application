using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
namespace GSUACM.ViewModels
{
    class EditProfileViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand UpdateProfileCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public string email { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string phone { get; set; }
        private DataTable table = new DataTable();
        public EditProfileViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.CancelCommand = new Command(this.buttonCancel_Click);
            this.UpdateProfileCommand = new Command(this.updateProfile);
        }
        private async void buttonCancel_Click()
        {
            await this.Navigation.PopModalAsync();
        }

        private async void updateProfile()
        {
            DB db = new DB();
            //Console.WriteLine("Server"+db.openConnection());
            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
           
            String firstname = fname;
            String lastname = lname;
            String emailadd = email;
            String phonenum = phone;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
          
            MySqlCommand command = new MySqlCommand("update user set email = @email,phone = @phone,fname = @fname,lname = @lname where userID = @userid", db.getConnection());
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value =firstname;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lastname;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailadd;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phonenum;
            command.Parameters.Add("@userid", MySqlDbType.VarChar).Value = Services.GlobalVars.userid;
            db.openConnection();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            int y = table.Rows.Count;
            if (checkTextBoxesValues() == true && y == 0)
            {
                // check if the password equal the confirm password
                if (phone.ValidatePhoneNumber(true) && email.IsValidEmail())
                {
                    // execute the query
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Services.GlobalVars.fname = firstname;
                        Services.GlobalVars.lname = lastname;
                        Services.GlobalVars.email = emailadd;
                        Services.GlobalVars.phone = phonenum;
                        MessagingCenter.Send<EditProfileViewModel, string>(this, "Hi", "John");
                        await Application.Current.MainPage.DisplayAlert("Your Account Has Been Created", "Account Created", "Ok");
                       await  Navigation.PopModalAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("ERROR", "Account Failed to Create", "Ok");
                    }

                }
                else
                {
                
                    if (phone.ValidatePhoneNumber(true) == false)
                    {
                       await Application.Current.MainPage.DisplayAlert("Invalid phone number", "phone Number Error", "Ok");
                    }
                    else if (email.IsValidEmail() == false)
                    {
                       await  Application.Current.MainPage.DisplayAlert("Invalid Email", "Email", "Ok");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Password must contain between 8-16 characters", "Password error", "Ok");
                    }
                }
            }
            else
            {
               await Application.Current.MainPage.DisplayAlert("Enter Your Information First Or Email Already Used", "Error", "Ok");
            }



        }
        private async void changePassword()
        {
            await this.Navigation.PopModalAsync();
        }
        private async void displayEmail()
        {
            await this.Navigation.PopModalAsync();
        }
        private async void displayPhone()
        {
            await this.Navigation.PopModalAsync();
        }
        public Boolean checkTextBoxesValues()
        {

            if (String.IsNullOrEmpty(fname))
            {
                Console.WriteLine("The email is " + GSUACM.Services.GlobalVars.fname);
                return false;
            }
            if (String.IsNullOrEmpty(lname))
            {
                return false;
            }
            if (String.IsNullOrEmpty(phone))
            {
                return false;
            }
            if (String.IsNullOrEmpty(email))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
