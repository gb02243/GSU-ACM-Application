using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using System.Data;

namespace GSUACM.ViewModels
{
    public class MemberListViewModel
    {
        public List<User>Members { get; set; }
        public List<User>Mentors { get; set; }
        public INavigation Navigation { get; set; }
        private DataTable queryResults { get; set; }
        public MemberListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Members = new List<User>();
            Mentors = new List<User>();
            GetMembers();
            //SimulateMembers(20);
        }

        private void GetMembers()
        {
            DB db = new DB();
            queryResults = new DataTable();
            if (db.openConnection() == false)
            {
                db.closeConnection();
                Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                // create the adapter and query
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT userID, fname, lname, title from user", db.getConnection());
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
                        Members.Add(user);
                    }
                    db.closeConnection();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Server Error", "No users were returned from the server.", "Ok");
                }
                db.closeConnection();
            }
            db.closeConnection();

            GetMentors();
        }

        private void GetMentors()
        {
            for (int i = 0; i < Members.Count; i++)
            {
                if(Members[i].title == "Mentor")
                {
                    Mentors.Add(Members[i]);
                }
            }
        }

        private void SimulateMembers(int members)
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
        }
    }
}
