using GSUACM.Models.ChatModels;
using GSUACM.Services;
using GSUACM.ViewModels.ChatViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSUACM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatListPage : ContentPage
    {
        public List<Chat> Chats { get; set; }
        public ChatListPage()
        {
            InitializeComponent();
            Chats = new List<Chat>(MockChatData.Get());
            chatsList.ItemsSource = Chats;
        }
    }
}