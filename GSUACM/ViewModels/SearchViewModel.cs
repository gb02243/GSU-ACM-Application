using GSUACM.Models;
using GSUACM.Services;
using GSUACM.Views;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public string EntryFirst { get; set; }
        public string TitleFind { get; set; }
        public string ResultFirst { get; set; }
        public string ResultLast { get; set; }
        public string ResultUserID { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SelectUserCommand { get; set; }
        public ObservableCollection<User> SearchResults { get; set; }
        public DataTable QueryResults { get; private set; }

        public SearchViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            CloseWindowCommand = new Command(CloseWindow);
            SearchCommand = new Command(SearchDatabase);
            SelectUserCommand = new Command<User>(SelectUser);
            SearchResults = new ObservableCollection<User>();

        }

        private async void SelectUser(User user)
        {
            GlobalVars.SelectedUser = user;
            await Navigation.PushModalAsync(new NavigationPage(new viewOtherUserProfile()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CloseWindow()
        {
            Navigation.PopModalAsync();
        }
        public async void SearchDatabase()
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
                if (EntryFirst != null && TitleFind != null)
                {
                    // create the adapter and query
                    MySqlCommand command = new MySqlCommand("SELECT fname, lname, userID, title FROM user WHERE fname LIKE @entryfirst OR title LIKE @title OR lname LIKE @entryfirst", db.getConnection());
                    command.Parameters.Add("@entryfirst", MySqlDbType.VarChar).Value = EntryFirst;
                    command.Parameters.Add("@title", MySqlDbType.VarChar).Value = TitleFind;
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    db.openConnection();
                    adapter.SelectCommand = command;

                    // set the adapter output
                    adapter.Fill(QueryResults);

                    if (QueryResults.Rows.Count > 0)
                    {
                        SearchResults = new ObservableCollection<User>();
                        // convert the query result table into a list
                        for (int i = 0; i < QueryResults.Rows.Count; i++)
                        {
                            User user = new User()
                            {
                                userID = QueryResults.Rows[i]["userID"].ToString(),
                                fname = QueryResults.Rows[i]["fname"].ToString(),
                                lname = QueryResults.Rows[i]["lname"].ToString(),
                                title = QueryResults.Rows[i]["title"].ToString()
                            };
                            SearchResults.Add(user);
                            PropertyChanged(this, new PropertyChangedEventArgs("SearchResults"));
                        }
                        db.closeConnection();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("No Results", "No users with that name were found.", "Ok");
                    }
                    db.closeConnection();

                }
                else if (EntryFirst != null && TitleFind == null)
                {
                    // create the adapter and query
                    MySqlCommand command = new MySqlCommand("SELECT fname, lname, userID, title FROM user WHERE fname LIKE @entryfirst OR lname LIKE @entryfirst", db.getConnection());
                    command.Parameters.Add("@entryfirst", MySqlDbType.VarChar).Value = EntryFirst;
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    db.openConnection();
                    adapter.SelectCommand = command;

                    // set the adapter output
                    adapter.Fill(QueryResults);

                    if (QueryResults.Rows.Count > 0)
                    {
                        SearchResults = new ObservableCollection<User>();
                        // convert the query result table into a list
                        for (int i = 0; i < QueryResults.Rows.Count; i++)
                        {
                            User user = new User()
                            {
                                userID = QueryResults.Rows[i]["userID"].ToString(),
                                fname = QueryResults.Rows[i]["fname"].ToString(),
                                lname = QueryResults.Rows[i]["lname"].ToString(),
                                title = QueryResults.Rows[i]["title"].ToString()

                            };
                            SearchResults.Add(user);
                            PropertyChanged(this, new PropertyChangedEventArgs("SearchResults"));
                        }
                        db.closeConnection();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("No Results", "No users with that name were found.", "Ok");
                    }
                    db.closeConnection();
                }
                else if (TitleFind != null && EntryFirst == null)
                {
                    // create the adapter and query
                    MySqlCommand command = new MySqlCommand("SELECT fname, lname, userID, title FROM user WHERE title LIKE @title", db.getConnection());
                    command.Parameters.Add("@title", MySqlDbType.VarChar).Value = TitleFind;
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    db.openConnection();
                    adapter.SelectCommand = command;

                    // set the adapter output
                    adapter.Fill(QueryResults);

                    if (QueryResults.Rows.Count > 0)
                    {
                        SearchResults = new ObservableCollection<User>();
                        // convert the query result table into a list
                        for (int i = 0; i < QueryResults.Rows.Count; i++)
                        {
                            User user = new User()
                            {
                                userID = QueryResults.Rows[i]["userID"].ToString(),
                                fname = QueryResults.Rows[i]["fname"].ToString(),
                                lname = QueryResults.Rows[i]["lname"].ToString(),
                                title = QueryResults.Rows[i]["title"].ToString()
                            };
                            SearchResults.Add(user);
                            PropertyChanged(this, new PropertyChangedEventArgs("SearchResults"));
                        }
                        db.closeConnection();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("No Results", "No users with that name were found.", "Ok");
                    }
                    db.closeConnection();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Oops", "Please enter a first and/or a last name to search for.", "Ok");
                }

            }
        }
    }
}

