using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Models.ChatModels
{
    public class Message
    {
        public string roomId { get; set; }
        public string channel { get; set; }
        public string text { get; set; }
        public string alias { get; set; }
        public string emoji { get; set; }
        public string avatar { get; set; }
        public string textPreview => text.Substring(0, 30)+"...";
        public string dateTime { get; set; }

    }
}
