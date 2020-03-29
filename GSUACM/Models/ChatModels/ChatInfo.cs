using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models.ChatModels
{
    public class ChatInfo
    {
        public string Chat_ID { get; set; }
        public string F_name { get; set; }
        public string L_name { get; set; }
        public string FullName => F_name + " " + L_name;
    }
}
