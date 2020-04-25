using GSUACM.Models;
using GSUACM.ViewModels.ControlPanel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GSUACM.ViewModels
{
    class EventsPageViewModel
    {
        public ObservableCollection<Event> EventsCollection { get; private set; }
        public ICommand OpenMapsCommand { get; set; }
        private DataTable QueryResults { get; set; }
        public EventsPageViewModel()
        {
            OpenMapsCommand = new Command<string>(OpenMaps);
            GetEventsFromDatabase();
            MessagingCenter.Subscribe<EventsPanelViewModel>(this, "event", (sender) =>
            {
                GetEventsFromDatabase();
            });
        }

        private async void OpenMaps(string location)
        {
            if (location.Contains(" "))
                location.Replace(" ", "+");
            if (Device.RuntimePlatform == Device.iOS)
            {
                await Launcher.OpenAsync("http://maps.apple.com/?q="+location);
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                await Launcher.OpenAsync("geo:0,0?q="+location);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Couldn't Open Maps App", "Ok");
            }
        }

        private async void GetEventsFromDatabase()
        {
            EventsCollection = new ObservableCollection<Event>();
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
                MySqlCommand command = new MySqlCommand("SELECT * FROM event ORDER BY eventID DESC", db.getConnection());
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
                        EventsCollection.Add(new Event()
                        {
                            EventID = QueryResults.Rows[i]["eventID"].ToString(),
                            Title = QueryResults.Rows[i]["title"].ToString(),
                            Location = "Location:\n"+QueryResults.Rows[i]["location"].ToString(),
                            PostDate = "Posted:\n"+QueryResults.Rows[i]["postdate"].ToString(),
                            Date = "Event Date: "+QueryResults.Rows[i]["date"].ToString(),
                            Body = QueryResults.Rows[i]["body"].ToString(),
                            UserID = QueryResults.Rows[i]["userID"].ToString(),
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
    }
}
