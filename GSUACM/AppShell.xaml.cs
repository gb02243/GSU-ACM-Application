using GSUACM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSUACM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        HomePage home = new HomePage();
       

        public AppShell(HomePage h)
        {
            Routing.RegisterRoute("home",typeof(HomePage));
            home = h;
          
            
            InitializeComponent();
            labelLogout_Click();
        }
        private async void labelLogout_Click()
        {
            await this.Navigation.PushAsync(home);
        }
    }
}