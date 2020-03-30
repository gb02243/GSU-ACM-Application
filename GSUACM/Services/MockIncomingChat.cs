using GSUACM.Models.ChatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSUACM.Services
{
    class MockIncomingChat
    {
        static List<ChatPreview> ChatList;
        public static void MockIncomingMessage(int messages, int people)
        {
            ChatList = new List<ChatPreview>();
            if(messages > 0 && people > 0)
            {
                for (int i = 0; i < messages; i++)
                {
                    for (int j = 0; j < people; j++)
                    {
                        //new Message() { Chat_ID = people.ToString(), User_ID = "1", DateTime = "10:00 PM", Message_Text = "Message " + messages.ToString() };
                        ChatList.Add(new ChatPreview() { Chat_ID = people.ToString(), F_name = "Person", L_name = people.ToString(), Msg_Preview = "Message " + messages.ToString() });
                    }
                }
            }
        }
        public static IEnumerable<ChatPreview> Get()
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
