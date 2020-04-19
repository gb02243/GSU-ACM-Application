using GSUACM.Services;
using GSUACM.Views.Control_Panel;
using GSUACM.Views.Control_Panel.Attendance;
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
        public ICommand OpenAttendanceCommand { get; set; }
        public ICommand OpenTitlesCommand { get; set; }
        public ICommand OpenNewsCommand { get; set; }
        public ControlPanelViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            CreatePollCommand = new Command(OpenCreatePoll);
            CreateEventCommand = new Command(OpenCreateEvent);
            OpenTitlesCommand = new Command(OpenUserTitles);
            OpenNewsCommand = new Command(OpenNews);
            OpenAttendanceCommand = new Command(OpenAttendance);
            if (GlobalVars.User == null || !GlobalVars.User.isAdmin)
                GoHome();
        }

        private async void OpenAttendance()
        {
            await Navigation.PushModalAsync(new NavigationPage(new AttendancePanelPage()));
        }

        private async void OpenNews()
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewsPanelPage()));
        }

        private async void OpenUserTitles()
        {
            await Navigation.PushModalAsync(new NavigationPage(new TitlesPanelPage()));
        }

        private async void GoHome()
        {
            await Application.Current.MainPage.DisplayAlert("Oops!", "You must be logged in as an Adminstrator to access this page.", "Ok");
            Application.Current.MainPage = new AppShell();
        }

        private async void OpenCreateEvent()
        {
            await Navigation.PushModalAsync(new NavigationPage(new EventsPanelPage()));
        }

        public async void OpenCreatePoll()
        {
            await Navigation.PushModalAsync(new NavigationPage(new PollsPanelPage()));
        }
    }
}
