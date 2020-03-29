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
                new Chat(){ chat_id="1" },
                new Chat(){ chat_id="2" },
                new Chat(){ chat_id="3" },
                new Chat(){ chat_id="4" },
                new Chat(){ chat_id="5" },
                new Chat(){ chat_id="6" },
                new Chat(){ chat_id="7" },
                new Chat(){ chat_id="8" },
                new Chat(){ chat_id="9" },
                new Chat(){ chat_id="10" },
                new Chat(){ chat_id="11" },
                new Chat(){ chat_id="12" }
            };
        }
    }
}
