using GSUACM.Models.ChatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSUACM.Services
{
    class MockIncomingChat
    {
        static List<Message> MessageList;
        public static void MockIncomingMessage(int messages, int people)
        {
            MessageList = new List<Message>();
            for (int i = 1; i <= people; i++)
            {
                for (int j = 1; j <= messages; j++)
                {
                    string fullMessage = "Message " + j.ToString() + ": Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
                    // Check if there is already a message in the conversation
                    var item = MessageList.SingleOrDefault(x => x.roomId == i.ToString());
                    if (item != null)
                        MessageList.Remove(item);
                    MessageList.Add(new Message() {roomId = people.ToString(),  });
                }
            }
        }
        public static IEnumerable<Message> Get()
        {
            return MessageList;
        }
        public static void Remove(string chat_id)
        {
            var item = MessageList.SingleOrDefault(x => x.roomId == chat_id);
            if (item != null)
                MessageList.Remove(item);
        }
    }
}
