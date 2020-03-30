using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GSUACM.Models.ChatModels;
using GSUACM.Services;
using GSUACM.Views.Chat;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ChatViewModels
{
    public class ChatListViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public List<ChatPreview> Chats { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand OpenChatCommand { get; private set; }
        public ChatListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            // TODO: get chat data from database
            MockIncomingChat.MockIncomingMessage(2, 10);
            Chats = new List<ChatPreview>(MockIncomingChat.Get());

            DeleteCommand = new Command<string>(DeleteChat);
            OpenChatCommand = new Command<string>(OpenChat);
        }
        public void DeleteChat(string chat_id)
        {
            MockIncomingChat.Remove(chat_id);
            UpdateList();
            PropertyChanged(this, new PropertyChangedEventArgs("Chats"));
        }
        private void UpdateList()
        {
            Chats = new List<ChatPreview>(MockIncomingChat.Get());
        }
        public async void OpenChat(string chat_id)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ChatPage(chat_id)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
