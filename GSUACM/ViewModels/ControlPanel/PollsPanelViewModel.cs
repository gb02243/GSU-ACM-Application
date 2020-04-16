using GSUACM.Models;
using GSUACM.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using static GSUACM.Models.Poll;

namespace GSUACM.ViewModels.ControlPanel
{
    class PollsPanelViewModel
    {
        public string CreatedPollID { get; set; }
        public bool canCreatePoll { get; set; }
        public string PollTitle { get; set; }
        public Poll Poll { get; set; }
        public int optionsCounter { get; set; }
        public ObservableCollection<Option> PollOptions { get; set; }
        public string Date { get; set; }
        public INavigation Navigation { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand PostPollCommand { get; set; }
        public ICommand AddOptionCommand { get; set; }
        public ICommand RemoveOptionCommand { get; set; }
        public PollsPanelViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            CloseWindowCommand = new Command(CloseWindow);
            PostPollCommand = new Command(PostPoll);
            AddOptionCommand = new Command(AddOption);
            RemoveOptionCommand = new Command<string>(RemoveOption);
            Date = DateTime.Now.ToString().Substring(0,9);
            CreatedPollID = Guid.NewGuid().ToString().Substring(0,8);

            PollOptions = new ObservableCollection<Option>();
            PollOptions.Add(new Option() { PollID = CreatedPollID, OptionID = CreatedPollID.Substring(0, 8) + "_" + optionsCounter++, OptionPlaceHolder = "Option " + (optionsCounter).ToString() });
            PollOptions.Add(new Option() { PollID = CreatedPollID, OptionID = CreatedPollID.Substring(0, 8) + "_" + optionsCounter++, OptionPlaceHolder = "Option " + (optionsCounter).ToString() });
            canCreatePoll = false;
        }

        private void AddOption()
        {
            if (PollOptions.Count < 10)
            {
                PollOptions.Add(new Option() { PollID = CreatedPollID, OptionID = CreatedPollID.Substring(0,8) + "_" + optionsCounter++, OptionPlaceHolder = "Option " + (optionsCounter).ToString() });
                //optionsCounter++;
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Cannot Add Option", "You have reached the maximum number of options.", "Ok");
            }
        }

        private void RemoveOption(string s)
        {
            Console.WriteLine("Trying to remove option");
            if(PollOptions.Count <= 2)
            {
                Application.Current.MainPage.DisplayAlert("Cannot Remove Option", "There must be at least 2 options in a poll!", "Ok");
                Console.WriteLine("Blocked removal");
            }
            else
            {
                var item = PollOptions.SingleOrDefault(x => x.OptionPlaceHolder == s);
                if (item != null)
                {
                    PollOptions.Remove(item);
                    item = null;
                }
            }
        }

        private async void PostPoll()
        {
            CreatePollObject();
            TestPrintPoll();
            if (canCreatePoll)
            {
                SendToDatabase();
                await Application.Current.MainPage.DisplayAlert("Poll Created", "You can view your poll under the \"Polls\" tab in the menu.", "Ok");
                await Navigation.PopModalAsync();
            }
            else
                await Application.Current.MainPage.DisplayAlert("Oops!", "Make sure you completed all of the poll fields!", "Ok");
        }

        private async void SendToDatabase()
        {
            DB db = new DB();
            if(db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                // create the adapter and query
                MySqlCommand command = new MySqlCommand("INSERT INTO poll(pollID, pollAuthorID, title, date) VALUES (@pollID, @pollAuthorID, @title, @date)", db.getConnection());
                command.Parameters.Add("@pollID", MySqlDbType.VarChar).Value = Poll.PollID;
                command.Parameters.Add("@pollAuthorID", MySqlDbType.VarChar).Value = Poll.PollAuthorID;
                command.Parameters.Add("@title", MySqlDbType.VarChar).Value = Poll.Title;
                command.Parameters.Add("@date", MySqlDbType.VarChar).Value = Poll.Date;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                //Console.WriteLine("Command Created");
                db.openConnection();
                adapter.SelectCommand = command;

                if(command.ExecuteNonQuery() == 1)
                {
                    foreach(Option o in Poll.Options.ToList())
                    {
                        // create the adapter and query
                        MySqlCommand command2 = new MySqlCommand("INSERT INTO option(optionID, pollID, text) VALUES (@optionID, @pollID, @text)", db.getConnection());
                        command2.Parameters.Add("@optionID", MySqlDbType.VarChar).Value = o.OptionID;
                        command2.Parameters.Add("@pollID", MySqlDbType.VarChar).Value = o.PollID;
                        command2.Parameters.Add("@text", MySqlDbType.VarChar).Value = o.Text;
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        //Console.WriteLine("Command Created");
                        adapter2.SelectCommand = command2;
                        if(command2.ExecuteNonQuery() != 1)
                        {
                            await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
                            break;
                        }
                    }
                }else
                    await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
                // send the query
                db.closeConnection();
            }
            db.closeConnection();
        }

        private void TestPrintPoll()
        {
            Console.WriteLine("-----Poll Content-----\nID: "+Poll.PollID+"\nTitle: " + Poll.Title + "\n\tOptions:");
            foreach (Option o in Poll.Options.ToList())
            {
                Console.WriteLine("\t\t- ID: "+o.OptionID+"\n\t\t\t - Text: "+o.Text);
            }
        }

        private void CreatePollObject()
        {
            bool minOptionsHaveText = false;
            int countText = 0;
            Poll = new Poll();
            //TODO: integrate with user system
            Poll.PollAuthorID = GlobalVars.User.userID;
            Poll.Date = Date;
            Poll.PollID = CreatedPollID;
            Poll.Title = PollTitle;
            Poll.Options = PollOptions;

            foreach(Option o in Poll.Options.ToList())
            {
                if(o.Text != null)
                {
                    countText++;
                }
                if (countText >= 2)
                    minOptionsHaveText = true;
                else
                    minOptionsHaveText = false;
            }

            if (Poll.Title == null || Poll.Options.Count < 2 || !minOptionsHaveText)
                canCreatePoll = false;
            else
            {
                canCreatePoll = true;
                foreach (Option o in Poll.Options.ToList())
                {
                    if (o.Text == null || o.Text.Length < 1)
                        Poll.Options.Remove(o);
                }
            }

            
        }

        private void CloseWindow()
        {
            Poll = null;
            PollOptions = null;
            Navigation.PopModalAsync();
        }
    }
}
