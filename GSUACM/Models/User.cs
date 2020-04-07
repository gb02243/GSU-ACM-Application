using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models
{
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
        public string boardTitle { get; set; }
        public string userID { get; set; }
    }
}
