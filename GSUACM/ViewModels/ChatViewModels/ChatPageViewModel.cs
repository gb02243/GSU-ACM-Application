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
        public ICommand OnSendCommand { get; set; }
        public ChatPageViewModel(INavigation navigation, string id, bool isChannel)
        {
            this.Navigation = navigation;
            BackCommand = new Command(CloseModal);

            //UpdateChat(id, isChannel);

            OnSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    //TODO: fix for new user class
                    if(isChannel)
                        Messages.Insert(0, new Message() { text = TextToSend, alias = GlobalVars.User.userID, channel = id });
                    else
                        Messages.Insert(0, new Message() { text = TextToSend, alias = GlobalVars.User.userID, roomId = id });
                    TextToSend = string.Empty;
                }

            });
        }
        public void UpdateChat(string id, bool isChannel)
        {
            //TODO: get chat data from API
            //if (isChannel)
            //    Messages = new ObservableCollection<Message>(MockIncomingMessage.GetChannel(id));
            //else
            //    Messages = new ObservableCollection<Message>(MockIncomingMessage.GetChat(id));
        }

        public void CloseModal()
        {
            Messages.Clear();
            MockIncomingMessage.ClearChannel();
            MockIncomingMessage.ClearChat();
            
            Navigation.PopModalAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
