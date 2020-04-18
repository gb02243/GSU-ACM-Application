using GSUACM.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GSUACM.Views
{
    public partial class editProfile : ContentPage
    {
        public editProfile()
        {
            InitializeComponent();
            BindingContext = new EditProfileViewModel(this.Navigation);
        }

    }
}
