using GSUACM.Models.ChatModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GSUACM.Services
{
    public static class ChatSimulator
    {
        public static ObservableCollection<Message> Messages { get; set; }
        // simulate {messages} messages from {people} people
        public static void Simulate(int messages, int people)
        {
            Messages = new ObservableCollection<Message>();
            for (int i = 0; i < people; i++)
            {
                for (int j = 0; j < messages; j++)
                {
                    Messages.Add(new Message() {
                        RoomID = i.ToString(),
                        SenderID = i.ToString(),
                        RecieverID = "6",
                        DateTime = DateTime.Now.ToString(),
                        MessageID = Guid.NewGuid().ToString(),
                        Text = "Test Message: "+j+" from Person: "+i,
                        IsChannel = false
                    });
                }
            }
        }
    }
}
