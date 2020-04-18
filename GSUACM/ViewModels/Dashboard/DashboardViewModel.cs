using GSUACM.Models;
using GSUACM.Services;
using GSUACM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using GSUACM.ViewModels;
using GSUACM.ViewModels.ControlPanel;
using System.Data;
using MySql.Data.MySqlClient;

namespace GSUACM.ViewModels.Dashboard
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        //public string WelcomeMessage { get; set; } 
        public ObservableCollection<NewsItem> NewsItems { get; set; }
        public string ToolbarText { get; set; }
        public string WelcomeMessage { get; set; }
        public bool isLoggedIn { get; set; }
        public ICommand ToolbarCommand { get; set; }
        public DataTable QueryResults { get; set; }
        public DashboardViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            MessagingCenter.Subscribe<LoginViewModel, string>(this, "Hi", (sender, arg) =>
            {
                WelcomeMessage = "Welcome, " + GlobalVars.User.fname;
                isLoggedIn = true;
                ToolbarText = "Log Out";
            });

            MessagingCenter.Subscribe<NewsPanelViewModel>(this, "news", (sender) =>
            {
                UpdateNewsItems();
            });

            if (GlobalVars.User == null)
            {
                WelcomeMessage = "Welcome!\nPlease log in.";
                
                ToolbarText = "Log In";
                isLoggedIn = false;
            }
            else
            {
                isLoggedIn = true;
                ToolbarText = "Log Out";
            }

            ToolbarCommand = new Command(GetToolbarAction)
            UpdateNewsItems();
        }
        
        

        public event PropertyChangedEventHandler PropertyChanged;

     
        public async void GetToolbarAction()
        {
            if (!isLoggedIn)
                await Navigation.PushModalAsync(new LoginPage());
            else
            {
                GlobalVars.User = null;
                WelcomeMessage = "Welcome!\nPlease log in.";
                ToolbarText = "Log In";
                isLoggedIn = false;
                await Application.Current.MainPage.DisplayAlert("Logged Out", "You have successfully logged out.", "Ok");
                Application.Current.MainPage = new AppShell();
            }
        }
        
        public async void UpdateNewsItems()
        {
            NewsItems = new ObservableCollection<NewsItem>();
            DB db = new DB();
            QueryResults = new DataTable();
            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                // create the adapter and query for the polls list
                MySqlCommand command = new MySqlCommand("SELECT * FROM newsitem", db.getConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                db.openConnection();
                adapter.SelectCommand = command;
                // set the adapter output
                adapter.Fill(QueryResults);

                // fill events list
                if (QueryResults.Rows.Count > 0)
                {
                    for (int i = 0; i < QueryResults.Rows.Count; i++)
                    {
                        NewsItems.Add(new NewsItem()
                        {
                            Title = QueryResults.Rows[i]["title"].ToString(),
                            Author = QueryResults.Rows[i]["author"].ToString(),
                            Date = QueryResults.Rows[i]["date"].ToString(),
                            Body = QueryResults.Rows[i]["body"].ToString(),
                        });
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Server Error", "No events were returned from the server.", "Ok");
                }
                db.closeConnection();
            }
            db.closeConnection();
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }
}
