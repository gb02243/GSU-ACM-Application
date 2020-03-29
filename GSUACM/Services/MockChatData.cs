using GSUACM.Models.ChatModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Services
{
    public class MockChatData
    {
        public static IEnumerable<Chat> Get()
        {
            return new List<Chat>
            {
                new Chat(){ Chat_ID="1" },
                new Chat(){ Chat_ID="2" },
                new Chat(){ Chat_ID="3" },
                new Chat(){ Chat_ID="4" },
                new Chat(){ Chat_ID="5" },
                new Chat(){ Chat_ID="6" },
                new Chat(){ Chat_ID="7" },
                new Chat(){ Chat_ID="8" },
                new Chat(){ Chat_ID="9" },
                new Chat(){ Chat_ID="10" },
                new Chat(){ Chat_ID="11" },
                new Chat(){ Chat_ID="12" }
            };
        }
    }
}
