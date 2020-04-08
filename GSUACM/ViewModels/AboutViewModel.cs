using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GSUACM.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.acm.org/about-acm/about-the-acm-organization"));
        }

        public ICommand OpenWebCommand { get; }
    }
}