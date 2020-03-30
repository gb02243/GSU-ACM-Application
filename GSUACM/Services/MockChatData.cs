using GSUACM.Models.ChatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSUACM.Services
{
    public class MockChatData
    {
        static List<Chat> ChatList;

        public static void Fill()
        {
            ChatList = new List<Chat>
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

        public static void Fill(int count)
        {
            ChatList = new List<Chat>();
            for (int i = 1; i <= count; i++)
            {
                ChatList.Add(new Chat() { Chat_ID = i.ToString() });
            }
            
        }
        public static IEnumerable<Chat> Get()
        {
            return ChatList;
        }

        public static void Remove(string chat_id)
        {
            var item = ChatList.SingleOrDefault(x => x.Chat_ID == chat_id);
            if (item != null)
                ChatList.Remove(item);
        }
    }
}
