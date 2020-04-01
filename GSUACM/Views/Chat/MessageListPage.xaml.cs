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
    public partial class MessageListPage : ContentPage
    {
        public MessageListPage()
        {
            InitializeComponent();
            BindingContext = new MessageListViewModel(Navigation);
        }
    }
}