using GSUACM.Models.ChatModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSUACM.Services
{
    class MockIncomingChat
    {
        static List<ChatPreview> ChatList;

        public static void MockIncomingMessage(int messages, int people)
        {
            if(messages > 0 && people > 0)
            {
                for (int i = 0; i < messages; i++)
                {
                    for (int j = 0; j < people; j++)
                    {

                    }
                }
            }
        }
    }
}
