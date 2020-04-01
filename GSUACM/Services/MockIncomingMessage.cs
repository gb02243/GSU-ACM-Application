using GSUACM.Models.ChatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSUACM.Services
{
    class MockIncomingMessage
    {
        static List<Message> MessageList { get; set; }
        static List<Message> ChannelList { get; set; }

        public static void SimulateMessages(int messages, int people)
        {
            MessageList = new List<Message>();

            // simulate regular messages
            for (int i = 1; i <= people; i++)
            {
                for (int j = 1; j <= messages; j++)
                {
                    string fullMessage = "Message " + j.ToString() + ": Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
                    // Check if there is already a message in the conversation
                    var item = MessageList.SingleOrDefault(x => x.roomId == i.ToString());
                    if (item != null)
                        MessageList.Remove(item);
                    MessageList.Add(new Message() { roomId = i.ToString(), text = fullMessage, alias = "Person "+i });
                }
            }
        }
        public static void SimulateChannels(int messages, int channels, int people)
        {
            ChannelList = new List<Message>();
            // simulate channel messages
            for (int i = 1; i <= channels; i++)
            {
                for (int j = 1; j <= messages; j++)
                {
                    for (int k = 0; k < people; k++)
                    {
                        string fullMessage = "Message " + j.ToString() + ": Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
                        // Check if there is already a message in the conversation
                        var item = ChannelList.SingleOrDefault(x => x.channel == i.ToString());
                        if (item != null)
                            ChannelList.Remove(item);
                        ChannelList.Add(new Message() { channel = i.ToString(), alias="Person "+k, text = "Person "+k+": "+fullMessage });
                    }
                }
            }
        }
        public static IEnumerable<Message> GetMessages()
        {
            return MessageList;
        }
        public static IEnumerable<Message> GetChannels()
        {
            return ChannelList;
        }
        public static void RemoveChat(string roomId)
        {
            var item = MessageList.SingleOrDefault(x => x.roomId == roomId);
            if (item != null)
                MessageList.Remove(item);
        }
        public static void RemoveChannel(string channel)
        {
            var item = ChannelList.SingleOrDefault(x => x.channel == channel);
            if (item != null)
                ChannelList.Remove(item);
        }
    }
}
