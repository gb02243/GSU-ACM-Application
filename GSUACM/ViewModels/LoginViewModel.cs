
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Input;
using GSUACM.ViewModels;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using System.Diagnostics;
using System.Data;
using GSUACM.Services;
using GSUACM;
using GSUACM.Views;
using GSUACM.Models;
using System.Collections.ObjectModel;

namespace GSUACM.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string email { get; set; }
        public INavigation Navigation { get; set; }
        public string password { get; set; }
        public ICommand SignInCommand { get; set; }
        public string WelcomeMessage { get; set; }
        public ICommand LogInCommand { get; set; }
        public ICommand SetUpDashBoard { get; set; }
        public ICommand CancelCommand { get; set; }
        private ObservableCollection<Request> request;
        private DataTable table = new DataTable();
        private DataTable table2 = new DataTable();
        //string conStr = ConfigurationManager.ConnectionStrings["MembersConnectionString"].ConnectionString;
        // label close CLICK
        public LoginViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.SignInCommand = new Command(this.labelGoToSignUp_Click);

            this.LogInCommand = new Command(this.buttonLogin_Click);
            this.CancelCommand = new Command(this.buttonCancel_Click);
        }

        private void buttonCancel_Click(object obj)
        {
            Navigation.PopModalAsync();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            //this.Close();
            //Application.Exit();
        }


        // button login
        private void buttonLogin_Click()
        {
            DB db = new DB();
            //Console.WriteLine("Server"+db.openConnection());
            if (db.openConnection() == false)
            {
                db.closeConnection();
                Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlDataAdapter adapterSession = new MySqlDataAdapter();
                String emailAddress = email;
                String pword = password;
                MySqlCommand command = new MySqlCommand("SELECT * FROM user WHERE email = @email and password = @pass", db.getConnection());
                MySqlCommand commandSession = new MySqlCommand("select * from tutorsession", db.getConnection());
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
                db.openConnection();
                adapter.SelectCommand = command;

                adapter.Fill(table);
                adapterSession.SelectCommand = commandSession;

                adapterSession.Fill(table2);
                request = new ObservableCollection<Request>();
                for (int i = 0; i < table2.Rows.Count; i++)
                {
                    Console.WriteLine("This is the date "+ table2.Rows[i]["subject"].ToString());
                    //request.sessionID = Convert.ToInt32(table2.Rows[i]["sessionID"]);

                    request.Add(new Request() { sessionID = Convert.ToInt32(table2.Rows[i]["sessionID"]) ,subject = table2.Rows[i]["subject"].ToString(), Date = table2.Rows[i]["date"].ToString() });

                }
                Services.GlobalVars.request = request;
                Console.WriteLine("The request is this long" + request);
                //check if the user exists or not
                if (table.Rows.Count > 0)
                {


                    App.InstantiateUser(table.Rows[0]["fname"].ToString(), table.Rows[0]["lname"].ToString(), table.Rows[0]["userID"].ToString());
                    //Console.WriteLine("This is the first name" + table.Rows[0]["fname"].ToString());

                    GSUACM.Services.GlobalVars.fname = table.Rows[0]["fname"].ToString();
                    GSUACM.Services.GlobalVars.email = table.Rows[0]["email"].ToString();
                    GSUACM.Services.GlobalVars.phone = table.Rows[0]["phone"].ToString();
                    GSUACM.Services.GlobalVars.clubpoints = table.Rows[0]["points"].ToString();
                    GSUACM.Services.GlobalVars.userid = Convert.ToInt32(table.Rows[0]["userID"]);
                    //Console.WriteLine("This is the first name" + GSUACM.Services.GlobalVars.fname);
                    db.closeConnection();
                    labelGoToHomePage_Click();
                    //WelcomeMessage = table.Rows[0]["fname"].ToString();
    }
                else
                {
                    // check if the username field is empty
                    if (emailAddress == null || emailAddress == "")
                    {
                        Application.Current.MainPage.DisplayAlert("Enter Your Username To Login", "Empty Username", "Ok");
                    }
                    // check if the password field is empty
                    else if (pword == null || pword == "")
                    {
                        Application.Current.MainPage.DisplayAlert("Enter Your Password To Login", "Empty Password", "Ok");
                    }

                    // check if the username or the password don't exist
                    else
                    {
                        Console.WriteLine(command);
                        Application.Current.MainPage.DisplayAlert("Wrong Username Or Password", "Wrong Data", "Ok");
                    }

                }
                db.closeConnection();
            }
            db.closeConnection();
        }
        // label go to signup CLICK
        private async void labelGoToSignUp_Click()
        {
            //NavigationPage.SetBackButtonTitle(this, "");
            await this.Navigation.PushModalAsync(new SignupPage());
        }

        // label go to signup CLICK
        private async void setUpUser()
        {
            //NavigationPage.SetBackButtonTitle(this, "");
            await this.Navigation.PushModalAsync(new SignupPage());
        }
        // label go to homepage CLICK
        public event EventHandler<EventArgs> OperationCompeleted;
        private async void labelGoToHomePage_Click()
        {
            
            MessagingCenter.Send<LoginViewModel ,string>(this, "Hi", "John");
            await this.Navigation.PopModalAsync();
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


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
