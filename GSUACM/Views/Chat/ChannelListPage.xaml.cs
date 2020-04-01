using GSUACM.ViewModels;
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
    public partial class ChannelListPage : ContentPage
    {
        public ChannelListPage()
        {
            InitializeComponent();
            BindingContext = new ChannelListViewModel(Navigation);
        }
    }
}