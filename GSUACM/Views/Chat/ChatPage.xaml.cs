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
        public ChatPage(string id, bool isChannel)
        {
            InitializeComponent();
            if (isChannel)
                BindingContext = new ChatPageViewModel(Navigation, id, true);
            else
                BindingContext = new ChatPageViewModel(Navigation, id, false);
        }
    }
}