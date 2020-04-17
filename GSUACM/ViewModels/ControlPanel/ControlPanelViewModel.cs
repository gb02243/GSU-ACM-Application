using GSUACM.Services;
using GSUACM.Views.Control_Panel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ControlPanel
{
    class ControlPanelViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand CreatePollCommand { get; set; }
        public ICommand CreateEventCommand { get; set; }
        public ControlPanelViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            CreatePollCommand = new Command(OpenCreatePoll);
            CreateEventCommand = new Command(OpenCreateEvent);
            if (GlobalVars.User == null)
                GoHome();
        }

        private async void GoHome()
        {
            await Application.Current.MainPage.DisplayAlert("Oops!", "You must be logged in as an Adminstrator to access this page.", "Ok");
            Application.Current.MainPage = new AppShell();
        }

        private async void OpenCreateEvent(object obj)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EventsPanelPage()));
        }

        public async void OpenCreatePoll()
        {
            await Navigation.PushModalAsync(new NavigationPage(new PollsPanelPage()));
        }
    }
}
