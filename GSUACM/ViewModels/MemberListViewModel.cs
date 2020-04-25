using GSUACM.Models;
using System;
using System.Text;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using System.Data;
using GSUACM.Services;
using System.Collections.ObjectModel;
using GSUACM.ViewModels.ControlPanel;
using System.ComponentModel;
using System.Windows.Input;
using GSUACM.Views;

namespace GSUACM.ViewModels
{
    public class MemberListViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<User>Members { get; set; }
        public ObservableCollection<User>Mentors { get; set; }
        public ICommand goToSearch { get; }
        public INavigation Navigation { get; set; }
        private DataTable queryResults { get; set; }
        public MemberListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.goToSearch = new Command(this.search);
            MessagingCenter.Subscribe<EditTitleResultsViewModel>(this, "title", (sender) =>
            {
                GetMembers();
                PropertyChanged(this, new PropertyChangedEventArgs("Members"));
                PropertyChanged(this, new PropertyChangedEventArgs("Mentors"));
            });
            MessagingCenter.Subscribe<EditProfileViewModel>(this, "member", (sender) =>
            {
                GetMembers();
                PropertyChanged(this, new PropertyChangedEventArgs("Members"));
                PropertyChanged(this, new PropertyChangedEventArgs("Mentors"));
            });
            if (GlobalVars.User == null)
                GoHome();
            else
            {
                GetMembers();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void search()
        {
            await Navigation.PushModalAsync(new searchPage());
        }

        private async void GoHome()
        {
            await Application.Current.MainPage.DisplayAlert("Oops!", "You must be logged in to access this page.", "Ok");
            Application.Current.MainPage = new AppShell();
        }

        private async void GetMembers()
        {
            Members = new ObservableCollection<User>();
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
                MySqlCommand command = new MySqlCommand("SELECT userID, fname, lname, title, image from user ORDER BY fname ASC", db.getConnection());
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
                            title = queryResults.Rows[i]["title"].ToString(),
                            ProfileImage = queryResults.Rows[i]["image"].ToString()
                        };
                        Members.Add(user);
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

            GetMentors();
        }

        private void GetMentors()
        {
            Mentors = new ObservableCollection<User>();
            for (int i = 0; i < Members.Count; i++)
            {
                if(Members[i].title == "Mentor")
                {
                    Mentors.Add(Members[i]);
                }
            }
        }

    /**    private void SimulateMembers(int members)
        {
            for (int i = 1; i <= members; i++)
            {
                Members.Add(new User()
                {
                    fname = "User",
                    lname = i.ToString(),
                    title = "Member"
                });
            }
            for (int i = 1; i <= members; i++)
            {
                if (i % 5 == 0)
                    Members[i-1].title = "Mentor";

                if (Members[i - 1].title == "Mentor")
                    Mentors.Add(Members[i - 1]); 
            }
        } **/
    }
}
