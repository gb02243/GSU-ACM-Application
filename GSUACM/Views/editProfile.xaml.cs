using DependencyServiceDemos;
using GSUACM.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        private async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                
                image.Source = ImageSource.FromStream(() => stream);
            }

        (sender as Button).IsEnabled = true;
        }
    }
}
