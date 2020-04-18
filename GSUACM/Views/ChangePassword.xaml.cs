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
using Xamarin.Forms;

namespace GSUACM.Views.Profile
{
    public partial class ChangePassword : ContentPage
    {
        public ChangePassword()
        {
            InitializeComponent();
            BindingContext = new ChangePasswordViewModel(this.Navigation);
        }
    }
}
 