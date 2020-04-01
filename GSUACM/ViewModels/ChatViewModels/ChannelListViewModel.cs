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
    class ChannelListViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public List<Message> Channels { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand OpenChatCommand { get; private set; }

        public ChannelListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            DeleteCommand = new Command<string>(DeleteChannel);
            OpenChatCommand = new Command<string>(OpenChannel);
            //TODO: get chat data from API
            MockIncomingMessage.SimulateChannels(5, 3, 2);
            UpdateList();
        }

        private void UpdateList()
        {
            Channels = new List<Message>(MockIncomingMessage.GetChannels());
            //PropertyChanged(this, new PropertyChangedEventArgs("Messages"));
        }

        public void DeleteChannel(string channel)
        {
            MockIncomingMessage.RemoveChannel(channel);
            UpdateList();
            //PropertyChanged(this, new PropertyChangedEventArgs("Messages"));
        }

        public async void OpenChannel(string channel)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ChatPage(channel, true)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
