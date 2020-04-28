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
    class NewsPanelViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand PostNewsCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public NewsItem NewsItem { get; set; }
        private bool canPostNews { get; set; }
        public string AuthorID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsBody { get; set; }
        public string NewsPostDate { get; set; }
        public NewsPanelViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            CloseWindowCommand = new Command(CloseWindow);
            PostNewsCommand = new Command(PostNews);
            if (GlobalVars.User == null || GlobalVars.User.isAdmin == false)
                GoHome();
            else
            {
                AuthorID = GlobalVars.User.userID;
                PostNewsCommand = new Command(PostNews);
                NewsPostDate = DateTime.Now.ToString().Substring(0, 9);
            }
        }
        private async void GoHome()
        {
            await Application.Current.MainPage.DisplayAlert("Oops!", "You must be logged in as an Administrator to access this page.", "Ok");
            Application.Current.MainPage = new AppShell();
        }

        private async void PostNews()
        {
            CreateNews();
            if (canPostNews)
            {
                SendToDatabase();
                MessagingCenter.Send(this, "news");
            }
            else
                await Application.Current.MainPage.DisplayAlert("Oops", "News item must be at least 120 characters long.", "Ok");
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
                MySqlCommand command = new MySqlCommand("INSERT INTO newsitem(title, author, date, body) VALUES (@title, @author, @date, @body)", db.getConnection());
                command.Parameters.Add("@title", MySqlDbType.VarChar).Value = NewsItem.Title;
                command.Parameters.Add("@author", MySqlDbType.VarChar).Value = NewsItem.Author;
                command.Parameters.Add("@date", MySqlDbType.VarChar).Value = NewsItem.Date;
                command.Parameters.Add("@body", MySqlDbType.VarChar).Value = NewsItem.Body;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                //Console.WriteLine("Command Created");
                db.openConnection();
                adapter.SelectCommand = command;
                // send the query
                if (command.ExecuteNonQuery() == 1)
                {
                    await Application.Current.MainPage.DisplayAlert("News Posted", "You can view your news posting under the \"News\" section in the dashboard.", "Ok");
                    await Navigation.PopModalAsync();
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
                db.closeConnection();
            }
        }

        private async void CreateNews()
        {
            canPostNews = false;
            NewsItem = new NewsItem()
            {
                Title = NewsTitle,
                Author = AuthorID,
                Body = NewsBody,
                Date = NewsPostDate,
            };
            Console.Write(NewsItem.Title +" "+NewsItem.Author+" "+NewsItem.Body+" "+NewsItem.Date);
            if(NewsItem.Body.Length < 120)
            {
                canPostNews = false;
            }
            else if(NewsItem.Author != null && NewsItem.Body != null && NewsItem.Date != null)
                canPostNews = true;
        }

        public async void CloseWindow()
        {
            await Navigation.PopModalAsync();
        }
    }
}
