using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models.ChatModels
{
    public class User
    {
        public string fName { get; set; }
        public string lName { get; set; }
        public string user_id { get; set; }
        public string fullName => fName + " " + lName;
    }
}
