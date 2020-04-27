using GSUACM.ViewModels.ChatViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSUACM.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public string RoomId { get; set; }
        public string Channel { get; set; }
        public string Chat_Title { get; set; }
        public ChatPage(string RoomID)
        {
            InitializeComponent();
            BindingContext = new ChatPageViewModel(Navigation, RoomID);
        }

        public void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            chatInput.UnFocusEntry();
        }

    }
}