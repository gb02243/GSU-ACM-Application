using GSUACM.Models.ChatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSUACM.Services
{
    class MockIncomingChat
    {
        // TODO: check if incoming message IsGroup and send to group preview page if true, else send to direct preview page
        static List<ChatPreview> ChatList;
        public static void MockIncomingMessage(int messages, int people)
        {
            ChatList = new List<ChatPreview>();
            for (int i = 1; i <= people; i++)
            {
                for (int j = 1; j <= messages; j++)
                {
                    string fullMessage = "Message " + j.ToString() + ": Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
                    // Check if there is already a message in the conversation
                    var item = ChatList.SingleOrDefault(x => x.Chat_ID == i.ToString());
                    if (item != null)
                        ChatList.Remove(item);
                    ChatList.Add(new ChatPreview() { Chat_ID = i.ToString(), F_name = "Person", L_name = i.ToString(), Msg_Preview = fullMessage.Substring(0, 30) + "..." }); ;
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
