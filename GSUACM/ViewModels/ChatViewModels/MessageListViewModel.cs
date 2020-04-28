using GSUACM.Models.ChatModels;
using GSUACM.Services;
using GSUACM.Views.Chat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ChatViewModels
{
    class MessageListViewModel
    {
        public INavigation Navigation { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand OpenChatCommand { get; private set; }

        public MessageListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            if (GlobalVars.User == null)
                GoHome();
            else
            {
                DeleteCommand = new Command<string>(DeleteChat);
                OpenChatCommand = new Command<string>(OpenChat);
                //TODO: get chat data from API
                ChatSimulator.Simulate(5, 5);
                UpdateList();

            }
        }

        private async void GoHome()
        {
            await Application.Current.MainPage.DisplayAlert("Oops!", "You must be logged in to access this page.", "Ok");
            Application.Current.MainPage = new AppShell();
        }

        private void UpdateList()
        {
            Messages = new ObservableCollection<Message>();
            foreach (Message message in ChatSimulator.Messages)
            {
                var item = Messages.SingleOrDefault(x => x.RoomID == message.RoomID);
                if (item != null)
                {
                    Messages.Remove(item);
                    item = null;
                }
                Messages.Add(message);
                //Console.WriteLine("Message Added");
            }
        }

        public void DeleteChat(string RoomID)
        {
            var item = Messages.SingleOrDefault(x => x.RoomID == RoomID);
            if (item != null)
            {
                Messages.Remove(item);
                item = null;
            }
            var item2 = ChatSimulator.Messages.SingleOrDefault(x => x.RoomID == RoomID);
            if (item2 != null)
            {
                ChatSimulator.Messages.Remove(item);
                item2 = null;
            }

            UpdateList();
        }

        public async void OpenChat(string RoomID)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ChatPage(RoomID)));
        }
    }
}
