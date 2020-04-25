using GSUACM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSUACM.Views
{
    public partial class pickPicturePage : ContentPage
    {
        public pickPicturePage()
        {
            InitializeComponent();
            BindingContext = new pickPictureViewModel(this.Navigation);
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
