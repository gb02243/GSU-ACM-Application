using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Services
{
    public static class GlobalVars
    {

        public static User User { get; set; }

        //TODO: retrieve all user info
        public static void InstantiateUser(string fname, string lname, string userID, string title, string isAdmin)
        {
            User = new User
            {
                fname = fname,
                lname = lname,
                userID = userID,
                title = title,
                isAdmin = bool.Parse(isAdmin)
            };
        }
    }

}
