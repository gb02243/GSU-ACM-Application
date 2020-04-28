using GSUACM.Models;
using GSUACM.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ControlPanel
{
    class EventsPanelViewModel
    {
        private Event Event { get; set; }
        private bool canPostEvent { get; set; }
        public INavigation Navigation { get; set; }
        public ICommand PostEventCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public string EventTitle { get; set; }
        public string EventLocation { get; set; }
        public string EventPostDate { get; set; }
        public string EventDate { get; set; }
        public string EventBody { get; set; }
        public string EventUserID { get; set; }
        public EventsPanelViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            CloseWindowCommand = new Command(CloseWindow);
            if(GlobalVars.User == null || GlobalVars.User.isAdmin == false)
                GoHome();
            else
            {
                PostEventCommand = new Command(PostEvent);
                EventUserID = GlobalVars.User.userID;
                EventPostDate = DateTime.Now.ToString().Substring(0, 9);
            }
        }

        private async void GoHome()
        {
            await Application.Current.MainPage.DisplayAlert("Oops!", "You must be logged in as an Administrator to access this page.", "Ok");
            Application.Current.MainPage = new AppShell();
        }

        private async void PostEvent(object obj)
        {
            CreateEvent();
            if (canPostEvent)
            {
                SendToDatabase();
                MessagingCenter.Send(this, "event");
            }
            else
                await Application.Current.MainPage.DisplayAlert("Oops!", "Make sure you filled out all of the fields!", "Ok");
        }

        private async void SendToDatabase()
        {
            DB db = new DB();
            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                // create the adapter and query
                MySqlCommand command = new MySqlCommand("INSERT INTO event(title, location, postdate, date, body, userID) VALUES(@title, @location, @postdate, @date, @body, @userID)", db.getConnection());
                command.Parameters.Add("@title", MySqlDbType.VarChar).Value = Event.Title;
                command.Parameters.Add("@location", MySqlDbType.VarChar).Value = Event.Location;
                command.Parameters.Add("@postdate", MySqlDbType.VarChar).Value = Event.PostDate;
                command.Parameters.Add("@date", MySqlDbType.VarChar).Value = Event.Date;
                command.Parameters.Add("@body", MySqlDbType.VarChar).Value = Event.Body;
                command.Parameters.Add("@userID", MySqlDbType.VarChar).Value = Event.UserID;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                //Console.WriteLine("Command Created");
                db.openConnection();
                adapter.SelectCommand = command;
                // send the query
                if (command.ExecuteNonQuery() == 1)
                {
                    await Application.Current.MainPage.DisplayAlert("Event Created", "You can view your event under the \"Events\" tab in the menu.", "Ok");
                    await Navigation.PopModalAsync();
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
                db.closeConnection();
            }
            db.closeConnection();
        }

        public void CreateEvent()
        {
            canPostEvent = false;
            Event = new Event()
            {
                Title= EventTitle,
                Location = EventLocation,
                PostDate = EventPostDate,
                Date = EventDate,
                Body = EventBody,
                UserID = EventUserID
            };

            if (Event.Title == null || Event.Location == null || Event.PostDate == null || Event.Date == null || Event.Body == null || Event.UserID == null || Event.Body.Length < 4)
                canPostEvent = false;
            else
                canPostEvent = true;
        }

        public async void CloseWindow()
        {
            await Navigation.PopModalAsync();
        }
    }
}
