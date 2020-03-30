using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using GSUACM.Models.ChatModels;
using GSUACM.Services;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ChatViewModels
{
    public class ChatListViewModel : INotifyPropertyChanged
    {
        public List<Chat> Chats { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public ChatListViewModel()
        {
            // TODO: get chat data from database
            MockChatData.Fill(5);
            Chats = new List<Chat>(MockChatData.Get());

            DeleteCommand = new Command<string>(DeleteChat);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void DeleteChat(string chat_id)
        {
            MockChatData.Remove(chat_id);
            UpdateList();
            PropertyChanged(this, new PropertyChangedEventArgs("Chats"));
        }
        private void UpdateList()
        {
            Chats = new List<Chat>(MockChatData.Get());
        }
    }
}
