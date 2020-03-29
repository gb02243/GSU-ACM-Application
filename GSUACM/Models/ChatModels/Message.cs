using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models.ChatModels
{
    public class Message
    {
        public string dateTime { get; set; }
        public string message_text { get; set; }
        public string chat_id { get; set; }
        public string user_id { get; set; }
    }
}
