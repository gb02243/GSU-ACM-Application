using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace GSUACM.Services
{
    public static class GlobalVars
    {
        public static User SelectedUser { get; set; }

        public static User User { get; set; }


        //TODO: retrieve all user info
        public static void InstantiateUser(string fname, string lname, string userID, string title, string isAdmin, string isTutor, string email, string phone, string points, string image)
        {
            User = new User
            {
                fname = fname,
                lname = lname,
                userID = userID,
                title = title,
                isAdmin = bool.Parse(isAdmin),
                isTutor = bool.Parse(isTutor),
                email = email,
                phone = phone,
                ClubPoints = points,
                ProfileImage = image
                
            };
            Application.Current.MainPage = new AppShell();
        }
        public static ObservableCollection<Request> request = new ObservableCollection<Request>();

    }
}
