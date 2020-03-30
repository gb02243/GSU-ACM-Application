using GSUACM.ViewModels.ChatViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSUACM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatListPage : ContentPage
    {
        public ChatListPage()
        {
            InitializeComponent();
            BindingContext = new ChatListViewModel(Navigation);
        }
    }
}