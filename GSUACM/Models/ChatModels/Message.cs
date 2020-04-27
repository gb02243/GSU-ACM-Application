using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models.ChatModels
{
    public class Message
    {
        public string MessageID { get; set; }
        public string RoomID { get; set; }
        public string Text { get; set; }
        public string DateTime { get; set; }
        public string SenderID { get; set; }
        public string RecieverID { get; set; }
        public bool IsChannel { get; set; }

    }
}
