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
    public partial class BecomeTutor : ContentPage
    {
        public BecomeTutor()
        {
            InitializeComponent();
            BindingContext = new BecomeTutorViewModel(this.Navigation);
        }
    }
}