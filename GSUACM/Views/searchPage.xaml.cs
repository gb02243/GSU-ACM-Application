using System;
using System.Collections.Generic;
using GSUACM.ViewModels.ControlPanel;
using Xamarin.Forms;

namespace GSUACM.Views
{
    public partial class searchPage : ContentPage
    {
        public searchPage()
        {
            InitializeComponent();
            BindingContext = new TitlesPanelViewModel(this.Navigation);
        }
    }
}
