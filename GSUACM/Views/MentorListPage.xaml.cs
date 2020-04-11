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
    public partial class MentorListPage : ContentPage
    {
        public MentorListPage()
        {
            InitializeComponent();
            BindingContext = new MemberListViewModel(Navigation);
        }
    }
}