using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
using GSUACM.ViewModels;
using Xamarin.Forms.Xaml;

namespace GSUACM.Views
{
   // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel(this.Navigation);
        }
    }
}