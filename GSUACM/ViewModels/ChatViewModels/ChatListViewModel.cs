using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GSUACM.Models.ChatModels;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ChatViewModels
{
    public class ChatListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Chat> Chats { get; set; } = new ObservableCollection<Chat>();

        public ChatListViewModel()
        {
            Chats.Add(new Chat() { chat_id = "1" });
            Chats.Add(new Chat() { chat_id = "2" });
            Chats.Add(new Chat() { chat_id = "3" });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
