using GSUACM.Models;
using GSUACM.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static GSUACM.Models.Poll;

namespace GSUACM.ViewModels
{
    public class PollsPageViewModel
    {
        public INavigation Navigation { get; set; }
        public ObservableCollection<Option> OptionsCollection { get; set; }
        public ObservableCollection<Poll> Polls { get; set; }
        public ObservableCollection<Poll> ActivePolls { get; set; }
        public ObservableCollection<Poll> PastPolls { get; set; }
        private DataTable QueryResults { get; set; }
        public PollsPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            if(GlobalVars.User == null) {
                GoHome();
            }
            else
            {
                OptionsCollection = new ObservableCollection<Option>();
                Polls = new ObservableCollection<Poll>();
                ActivePolls = new ObservableCollection<Poll>();
                PastPolls = new ObservableCollection<Poll>();

                GetPollsFromDatabase();
            }
        }

        private async void GoHome()
        {
            await Application.Current.MainPage.DisplayAlert("Oops!", "You must be logged in to access this page.", "Ok");
            Application.Current.MainPage = new AppShell();
        }

        private async void Vote(string optionID)
        {
            //await Application.Current.MainPage.DisplayAlert("test", optionID, "Ok");
            DB db = new DB();
            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                // create the adapter and query
                MySqlCommand command = new MySqlCommand("UPDATE option SET votes = votes+1 WHERE optionID LIKE(\""+optionID+"\")", db.getConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                //Console.WriteLine("Command Created");
                db.openConnection();
                adapter.SelectCommand = command;

                if (command.ExecuteNonQuery() == 1)
                {
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                    await Application.Current.MainPage.DisplayAlert("Thank you", "Vote Submitted", "Ok");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
                db.closeConnection();
            }
            db.closeConnection();
            RefreshPage();
        }

        private void RefreshPage()
        {
            Application.Current.MainPage = new AppShell();
        }

        private void CategorizePolls()
        {
            DateTime WeekFromToday = DateTime.Today.AddDays(7);
            foreach (Poll poll1 in Polls)
            {
                DateTime ConvertedDate = DateTime.Parse(poll1.Date);
                if (DateTime.Compare(WeekFromToday, ConvertedDate) > 0)
                {
                    PastPolls.Add(poll1);
                }
                else
                {
                    ActivePolls.Add(poll1);
                }
            }
        }

        public async void GetPollsFromDatabase()
        {
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
                MySqlCommand command = new MySqlCommand("SELECT * FROM poll p, option o WHERE p.pollID = o.pollID", db.getConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                db.openConnection();
                adapter.SelectCommand = command;
                // set the adapter output
                adapter.Fill(QueryResults);

                if (QueryResults.Rows.Count > 0)
                {
                    for (int i = 0; i < QueryResults.Rows.Count; i++)
                    {
                        Option option = new Option()
                        {
                            PollID = QueryResults.Rows[i]["pollID"].ToString(),
                            OptionID = QueryResults.Rows[i]["optionID"].ToString(),
                            Text = QueryResults.Rows[i]["text"].ToString(),
                            VoteCommand = new Command<string>(Vote),
                            Votes = QueryResults.Rows[i]["votes"].ToString()
                        };
                        OptionsCollection.Add(option);
                        var item = Polls.SingleOrDefault(x => x.PollID == QueryResults.Rows[i]["pollID"].ToString());
                        if (item == null)
                        {
                            Poll poll = new Poll()
                            {
                                PollAuthorID = QueryResults.Rows[i]["pollAuthorID"].ToString(),
                                PollID = QueryResults.Rows[i]["pollID"].ToString(),
                                Title = QueryResults.Rows[i]["title"].ToString(),
                                Date = QueryResults.Rows[i]["date"].ToString(),
                                Options = new ObservableCollection<Option>()
                            };
                            Polls.Add(poll);
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Server Error", "No polls were returned from the server.", "Ok");
                }

                foreach (Option option1 in OptionsCollection)
                {
                    foreach (Poll poll1 in Polls)
                    {
                        if (option1.PollID == poll1.PollID)
                            poll1.Options.Add(option1);
                    }
                }
                db.closeConnection();
            }
            db.closeConnection();

            CategorizePolls();
        }

        private void SimulatePoll(int polls, int options)
        {
            for (int i = 1; i <= polls; i++)
            {
                Polls.Add(new Poll
                {
                    Title = "Poll " + i,
                    Date = DateTime.Now.Date.ToString().Substring(0, 9),
                    Options = new ObservableCollection<Poll.Option>()
                });
            }
            for (int i = 1; i <= Polls.Count; i++)
            {
                for (int j = 1; j <= options; j++)
                {
                    Polls[i - 1].Options.Add(new Poll.Option() { Text = "Poll " + i + " Option " + j });
                }
            }
            for (int i = 1; i <= polls; i++)
            {
                if (i > 3)
                {
                    Polls[i - 1].isActive = true;
                }
            }
        }
    }
}
