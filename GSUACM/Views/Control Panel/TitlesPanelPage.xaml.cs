using GSUACM.ViewModels.ControlPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSUACM.Views.Control_Panel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitlesPanelPage : ContentPage
    {
        public TitlesPanelPage()
        {
            InitializeComponent();
            BindingContext = new TitlesPanelViewModel(Navigation);
        }
    }
}