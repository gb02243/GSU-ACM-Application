using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GSUACM.ViewModels
{
    public class MemberListViewModel
    {
        public List<User>Members { get; set; }
        public List<User>Mentors { get; set; }
        public INavigation Navigation { get; set; }
        public MemberListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Members = new List<User>();
            Mentors = new List<User>();
            SimulateMembers(20);
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
