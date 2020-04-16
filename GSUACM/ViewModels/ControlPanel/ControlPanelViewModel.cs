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
        public ICommand createPollCommand { get; set; }
        public ControlPanelViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            createPollCommand = new Command(OpenCreatePoll);

        }

        public async void OpenCreatePoll()
        {
            await Navigation.PushModalAsync(new NavigationPage(new PollsPanelPage()));
        }
    }
}
