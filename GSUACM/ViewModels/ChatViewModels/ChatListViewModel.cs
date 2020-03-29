using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GSUACM.Models.ChatModels;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ChatViewModels
{
    public class ChatListViewModel : BaseViewModel
    {        
        public ChatListViewModel()
        {
            OnPropertyChanged(nameof(Chats));
        }
        public ObservableCollection<Chat> Chats
        {
            get => chats;
            set => chats = value;
        }
        private ObservableCollection<Chat> chats = new ObservableCollection<Chat>
        {
            new Chat{ chat_id="1" },
            new Chat{ chat_id="2" },
            new Chat{ chat_id="3" },
            new Chat{ chat_id="4" },
            new Chat{ chat_id="5" },
            new Chat{ chat_id="6" }
        };
    }
}
