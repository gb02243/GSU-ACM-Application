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
    class GroupListViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        //TODO: link to chat API
        public List<ChatPreview> Groups { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand OpenChatCommand { get; private set; }

        public GroupListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            // TODO: get chat data from database
            MockIncomingChat.MockIncomingMessage(2, 10);
            Groups = new List<ChatPreview>(MockIncomingChat.Get());

            DeleteCommand = new Command<string>(DeleteGroup);
            OpenChatCommand = new Command<string>(OpenGroup);
        }
        public void DeleteGroup(string chat_id)
        {
            MockIncomingChat.Remove(chat_id);
            UpdateList();
            PropertyChanged(this, new PropertyChangedEventArgs("Groups"));
        }
        private void UpdateList()
        {
            Groups = new List<ChatPreview>(MockIncomingChat.Get());
        }
        public async void OpenGroup(string chat_id)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ChatPage(chat_id)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
