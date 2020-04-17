using GSUACM.Models.ChatModels;
using GSUACM.Services;
using GSUACM.Views.Chat;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ChatViewModels
{
    class MessageListViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public List<Message> Messages { get; set; }
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
                //MockIncomingMessage.SimulateMessages(2, 10);
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
            //Messages = new List<Message>(MockIncomingMessage.GetMessages());
            //PropertyChanged(this, new PropertyChangedEventArgs("Messages"));
        }

        public void DeleteChat(string roomId)
        {
            MockIncomingMessage.RemoveChat(roomId);
            UpdateList();
            //PropertyChanged(this, new PropertyChangedEventArgs("Messages"));
        }

        public async void OpenChat(string channel)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ChatPage(channel, false)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
