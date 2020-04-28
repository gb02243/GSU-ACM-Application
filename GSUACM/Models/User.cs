using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GSUACM.Models
{
    [Serializable]
    public class User
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string fullName => fname + " " + lname;
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool isTutor { get; set; }
        public bool isAdmin { get; set; }
        public bool isBoardMember { get; set; }
        public string title { get; set; }
        public string userID { get; set; }
        public string ClubPoints { get; set; }
        public string ProfileImage { get; set; }
        public ICommand SelectUserCommand { get; set; }
    }
}
