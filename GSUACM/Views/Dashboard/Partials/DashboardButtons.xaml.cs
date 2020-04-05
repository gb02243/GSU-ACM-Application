using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSUACM.Views.Dashboard.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardButtons : ContentView
    {
        public DashboardButtons()
        {
            InitializeComponent();
            //TODO: Set bindingcontext
        }
    }
}