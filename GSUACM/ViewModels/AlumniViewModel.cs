using System;
using System.Text;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using System.Data;
using GSUACM.Services;
using System.Collections.ObjectModel;
using GSUACM.ViewModels.ControlPanel;
using System.ComponentModel;
using GSUACM.Models;
using System.Windows.Input;
using GSUACM.Views;

namespace GSUACM.ViewModels
{
    public class AlumniViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<User> Alumni { get; set; }
        public ICommand goToSearch { get; }
        public INavigation Navigation { get; set; }
        private DataTable queryResults { get; set; }
        public AlumniViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.goToSearch = new Command(this.search);
           // MessagingCenter.Subscribe<EditTitleResultsViewModel>(this, "title", (sender) =>
            //{
              //  GetMembers();
                //PropertyChanged(this, new PropertyChangedEventArgs("Members"));
                //PropertyChanged(this, new PropertyChangedEventArgs("Mentors"));
            //});
            if (GlobalVars.User == null)
                GoHome();
            else
            {
                GetMembers();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void search()
        {
            await this.Navigation.PushModalAsync(new searchPage());
        }
        private async void GoHome()
        {
            await Application.Current.MainPage.DisplayAlert("Oops!", "You must be logged in to access this page.", "Ok");
            Application.Current.MainPage = new AppShell();
        }

        private async void GetMembers()
        {
            Alumni = new ObservableCollection<User>();
            DB db = new DB();
            queryResults = new DataTable();
            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                // create the adapter and query
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT userID, fname, lname, title from user where title = 'Alumni'", db.getConnection());
                //Console.WriteLine("Command Created");
                db.openConnection();
                adapter.SelectCommand = command;

                // set the adapter output
                adapter.Fill(queryResults);

                if (queryResults.Rows.Count > 0)
                {
                    // convert the query result table into a list
                    for (int i = 0; i < queryResults.Rows.Count; i++)
                    {
                        User user = new User()
                        {
                            userID = queryResults.Rows[i]["userID"].ToString(),
                            fname = queryResults.Rows[i]["fname"].ToString(),
                            lname = queryResults.Rows[i]["lname"].ToString(),
                            title = queryResults.Rows[i]["title"].ToString()
                        };
                        Alumni.Add(user);
                    }
                    db.closeConnection();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Server Error", "No users were returned from the server.", "Ok");
                }
                db.closeConnection();
            }
            db.closeConnection();

        
        }

    }
}