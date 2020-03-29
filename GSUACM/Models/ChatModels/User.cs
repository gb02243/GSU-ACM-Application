using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models.ChatModels
{
    public class User
    {
        public string F_name { get; set; }
        public string L_name { get; set; }
        public string User_ID { get; set; }
        public string FullName => F_name + " " + L_name;
    }
}
