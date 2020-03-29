using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models.ChatModels
{
    public class Message
    {
        public string DateTime { get; set; }
        public string Message_Text { get; set; }
        public string Chat_ID { get; set; }
        public string User_ID { get; set; }
    }
}
