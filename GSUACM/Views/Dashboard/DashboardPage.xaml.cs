using GSUACM.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_Login.ViewModels;

namespace GSUACM.Views.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {

        public DashboardPage()
        {
            InitializeComponent();
            //Console.WriteLine("Inside Dashboard.view "+Services.GlobalVars.fname);
            BindingContext = new DashboardViewModel(Navigation);
            
          
        }
    }
}