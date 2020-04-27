using GSUACM.Models.ChatModels;
using GSUACM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ChatViewModels
{
    class ChatPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand BackCommand { get; private set; }
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public string TextToSend { get; set; }
        public string Chat_Title { get; set; }
        public string RoomID { get; set; }
        public ICommand OnSendCommand { get; set; }
        public ChatPageViewModel(INavigation navigation, string RoomID)
        {
            this.Navigation = navigation;
            BackCommand = new Command(CloseModal);
            this.RoomID = RoomID;

            UpdateChat(RoomID);

            OnSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    ChatSimulator.Messages.Add(new Message() { Text = TextToSend, SenderID = GlobalVars.User.userID, RoomID = RoomID, MessageID = Guid.NewGuid().ToString() });
                    Messages.Add(new Message() { Text = TextToSend, SenderID = GlobalVars.User.userID, RoomID = RoomID, MessageID = Guid.NewGuid().ToString()});

                    TextToSend = string.Empty;
                }

            });
        }
        public void UpdateChat(string roomID)
        {
            //TODO: get chat data from API
            Messages = new ObservableCollection<Message>();
            foreach (Message message in ChatSimulator.Messages)
            {
                if(message.RoomID == roomID)
                {
                    Messages.Add(message);
                }
            }
            Chat_Title = Messages[0].RoomID;
        }

        public void CloseModal()
        {
            Messages.Clear();
            Navigation.PopModalAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
