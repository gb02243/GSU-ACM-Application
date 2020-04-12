using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace GSUACM.ViewModels
{
    public class AlumniViewModel
    {
    public List<User> Alumni { get; set; }
    public List<User> Mentors { get; set; }
    public INavigation Navigation { get; set; }
    public AlumniViewModel(INavigation navigation)
    {
        this.Navigation = navigation;
        Alumni = new List<User>();
        Alumni = new List<User>();
        SimulateMembers(20);
    }

    private void SimulateMembers(int members)
    {
        for (int i = 1; i <= members; i++)
        {
            Alumni.Add(new User()
            {
                fname = "User",
                lname = i.ToString(),
                title = "Member"
            });
        }
        for (int i = 1; i <= members; i++)
        {
            if (i % 5 == 0)
             Alumni[i - 1].title = "Mentor";

            if (Alumni[i - 1].title == "Mentor")
                Mentors.Add(Alumni[i - 1]);
        }
    }
}
}