using GSUACM.ViewModels;
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
    public partial class ViewMemberProfilePage : ContentPage
    {
        public ViewMemberProfilePage()
        {
            InitializeComponent();
            BindingContext = new viewOtherUserViewModel(Navigation);
        }
    }
}