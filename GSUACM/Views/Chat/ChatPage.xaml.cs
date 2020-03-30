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
        public string Chat_ID;
        public ChatPage(string chat_id)
        {
            InitializeComponent();
            this.Chat_ID = chat_id;
            BindingContext = new ChatPageViewModel(Navigation, Chat_ID);
        }
    }
}