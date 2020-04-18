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
    public partial class editProfile : ContentPage
    {
        public editProfile()
        {
            InitializeComponent();
            BindingContext = new editProfileViewModel(this.Navigation);
        }
    }
}
