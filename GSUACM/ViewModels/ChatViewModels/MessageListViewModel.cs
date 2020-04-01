using GSUACM.Models.ChatModels;
using GSUACM.Services;
using GSUACM.Views.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ChatViewModels
{
    class MessageListViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        //TODO: link to chat API
        public List<Message> Messages { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand OpenChatCommand { get; private set; }
        public MessageListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            // TODO: get chat data from database
            MockIncomingChat.MockIncomingMessage(2, 10);
            Messages = new List<Message>(MockIncomingChat.Get());

            DeleteCommand = new Command<string>(DeleteChat);
            OpenChatCommand = new Command<string>(OpenChat);
        }
        public void DeleteChat(string chat_id)
        {
            MockIncomingChat.Remove(chat_id);
            UpdateList();
            PropertyChanged(this, new PropertyChangedEventArgs("Messages"));
        }
        private void UpdateList()
        {
            Messages = new List<Message>(MockIncomingChat.Get());
        }
        public async void OpenChat(string chat_id)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ChatPage(chat_id)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
