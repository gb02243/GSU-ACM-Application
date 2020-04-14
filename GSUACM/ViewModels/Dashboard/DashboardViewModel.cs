using GSUACM.Models;
using GSUACM.Services;
using GSUACM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using GSUACM.ViewModels;

namespace GSUACM.ViewModels.Dashboard
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        //public string WelcomeMessage { get; set; } 
        public List<NewsItem> NewsItems { get; set; }
        public string ToolbarText { get; set; }
        public string WelcomeMessage { get; set; }
        public bool isLoggedIn { get; set; }
        public ICommand ToolbarCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public DashboardViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            MessagingCenter.Subscribe<LoginViewModel, string>(this, "Hi", (sender, arg) =>
            {
                WelcomeMessage = "Welcome, " + GlobalVars.User.fname;
                isLoggedIn = true;
                ToolbarText = "Log Out";
            });
            if (GlobalVars.User == null)
            {
                WelcomeMessage = "Welcome!\nPlease log in.";
                
                ToolbarText = "Log In";
                isLoggedIn = false;
            }
            else
            {
                isLoggedIn = true;
                ToolbarText = "Log Out";
               // WelcomeMessage;
                
            }

            ToolbarCommand = new Command(GetToolbarAction);
          
            UpdateCommand = new Command(UpdateDashboard);
            //TODO: fix for new user class
            //User = GlobalVars.User.userID;
            //WelcomeMessage = GlobalVars.User.fname;
            NewsItems = new List<NewsItem>();
            UpdateNewsItems();
        }
        
        //TODO: ask dylan if this is necessary
        //private string myStringProperty=GlobalVars.User.fname;
        //public string MyStringProperty
        //{

        //    get { return myStringProperty; }
        //    set
        //    {
        //        myStringProperty = value;
        //        OnPropertyChanged(nameof(MyStringProperty)); // Notify that there was a change on this property
        //    }
        //}
        

        public event PropertyChangedEventHandler PropertyChanged;

     
        public async void GetToolbarAction()
        {
            if (!isLoggedIn)
                await Navigation.PushModalAsync(new LoginPage());
            else
            {
                GlobalVars.User = null;
                WelcomeMessage = "Welcome!\nPlease log in.";
                ToolbarText = "Log In";
                isLoggedIn = false;
                Application.Current.MainPage.DisplayAlert("Logged Out", "You have successfully logged out.", "Ok");
            }
            
            // TODO: handle sign out
        }

        public void UpdateDashboard()
        {
            if (GlobalVars.User == null)
            {
                Console.WriteLine("This is name after logging in" + GlobalVars.User.fname);
                //WelcomeMessage = "Welcome!\nPlease log in.";
                WelcomeMessage = "Welcome, " + GlobalVars.User.fname;
                ToolbarText = "Log In";
                isLoggedIn = false;
            }
            else
            {
                Console.WriteLine("This is name after logging in" + GlobalVars.User.fname);
                isLoggedIn = true;
                ToolbarText = "Log Out";
                //WelcomeMessage = "Welcome, " + GlobalVars.User.fname;
               WelcomeMessage = "Welcome, " + GlobalVars.User.fname;
            }
        }
        
        public void UpdateNewsItems()
        {
            // TODO: get news items from database
            NewsItems.Add(new NewsItem()
                {
                    Title = "Test News Article 1",
                    Author = "First Last",
                    Date = "4/5/2020",
                    Time = "1:41 PM",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sit amet nibh id leo tempor feugiat et non tellus. " +
                    "Quisque turpis odio, dignissim et neque at, tristique condimentum odio. Sed fermentum nunc vitae fermentum facilisis. " +
                    "Nullam in massa fermentum, finibus sapien sit amet, eleifend urna. Cras eleifend rhoncus urna eu semper. " +
                    "Suspendisse tincidunt rutrum viverra. Fusce elit risus, vestibulum ac venenatis vel, facilisis vel arcu. " +
                    "In mollis posuere ipsum, ut condimentum sapien volutpat ac. Suspendisse eget cursus risus. Sed id ex nisi. " +
                    "Etiam eu ipsum non mi tristique blandit. Fusce lacinia dictum turpis, sit amet tristique dui congue a.\n\n" +
                    "Nam vestibulum vitae nibh id consectetur. Morbi iaculis eleifend fermentum. Cras quis libero nec dui feugiat commodo. " +
                    "Mauris diam felis, tincidunt nec cursus nec, rhoncus ac nisl. Aliquam ac mollis risus. Proin condimentum at orci vel dictum. " +
                    "Donec in faucibus ante, eget ullamcorper urna. Interdum et malesuada fames ac ante ipsum primis in faucibus. Morbi placerat porttitor auctor. " +
                    "Aliquam egestas non nisi eget convallis.\n\n" +
                    "Suspendisse ac risus hendrerit, dapibus lacus at, lacinia nulla. Mauris id congue purus, ac placerat arcu. Nulla convallis lacus vel feugiat posuere. " +
                    "Curabitur sem ipsum, rhoncus non est id, aliquam eleifend ligula. Cras quis dui eu diam feugiat vulputate eget non elit. Duis tempus congue ornare. " +
                    "Aliquam elit ligula, condimentum et urna sed, tincidunt viverra velit. Donec sagittis libero vitae arcu convallis molestie. " +
                    "Aenean a cursus urna, et efficitur lectus. Praesent ac sodales urna, vel eleifend lorem. Integer tortor sem, commodo ut augue vel, consectetur rhoncus ante. " +
                    "Nam vel lacus dui. Praesent pulvinar quis metus eget fringilla. Pellentesque dictum neque quis tellus consequat, sed consectetur dolor ornare."
            });
            NewsItems.Add(new NewsItem()
            {
                Title = "Test News Article 2",
                Author = "First Last",
                Date = "4/5/2020",
                Time = "1:41 PM",
                Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sit amet nibh id leo tempor feugiat et non tellus. " +
                    "Quisque turpis odio, dignissim et neque at, tristique condimentum odio. Sed fermentum nunc vitae fermentum facilisis. " +
                    "Nullam in massa fermentum, finibus sapien sit amet, eleifend urna. Cras eleifend rhoncus urna eu semper. " +
                    "Suspendisse tincidunt rutrum viverra. Fusce elit risus, vestibulum ac venenatis vel, facilisis vel arcu. " +
                    "In mollis posuere ipsum, ut condimentum sapien volutpat ac. Suspendisse eget cursus risus. Sed id ex nisi. " +
                    "Etiam eu ipsum non mi tristique blandit. Fusce lacinia dictum turpis, sit amet tristique dui congue a.\n\n" +
                    "Nam vestibulum vitae nibh id consectetur. Morbi iaculis eleifend fermentum. Cras quis libero nec dui feugiat commodo. " +
                    "Mauris diam felis, tincidunt nec cursus nec, rhoncus ac nisl. Aliquam ac mollis risus. Proin condimentum at orci vel dictum. " +
                    "Donec in faucibus ante, eget ullamcorper urna. Interdum et malesuada fames ac ante ipsum primis in faucibus. Morbi placerat porttitor auctor. " +
                    "Aliquam egestas non nisi eget convallis.\n\n" +
                    "Suspendisse ac risus hendrerit, dapibus lacus at, lacinia nulla. Mauris id congue purus, ac placerat arcu. Nulla convallis lacus vel feugiat posuere. " +
                    "Curabitur sem ipsum, rhoncus non est id, aliquam eleifend ligula. Cras quis dui eu diam feugiat vulputate eget non elit. Duis tempus congue ornare. " +
                    "Aliquam elit ligula, condimentum et urna sed, tincidunt viverra velit. Donec sagittis libero vitae arcu convallis molestie. " +
                    "Aenean a cursus urna, et efficitur lectus. Praesent ac sodales urna, vel eleifend lorem. Integer tortor sem, commodo ut augue vel, consectetur rhoncus ante. " +
                    "Nam vel lacus dui. Praesent pulvinar quis metus eget fringilla. Pellentesque dictum neque quis tellus consequat, sed consectetur dolor ornare."
            });
            NewsItems.Add(new NewsItem()
            {
                Title = "Test News Article 3",
                Author = "First Last",
                Date = "4/5/2020",
                Time = "1:41 PM",
                Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sit amet nibh id leo tempor feugiat et non tellus. " +
                    "Quisque turpis odio, dignissim et neque at, tristique condimentum odio. Sed fermentum nunc vitae fermentum facilisis. " +
                    "Nullam in massa fermentum, finibus sapien sit amet, eleifend urna. Cras eleifend rhoncus urna eu semper. " +
                    "Suspendisse tincidunt rutrum viverra. Fusce elit risus, vestibulum ac venenatis vel, facilisis vel arcu. " +
                    "In mollis posuere ipsum, ut condimentum sapien volutpat ac. Suspendisse eget cursus risus. Sed id ex nisi. " +
                    "Etiam eu ipsum non mi tristique blandit. Fusce lacinia dictum turpis, sit amet tristique dui congue a.\n\n" +
                    "Nam vestibulum vitae nibh id consectetur. Morbi iaculis eleifend fermentum. Cras quis libero nec dui feugiat commodo. " +
                    "Mauris diam felis, tincidunt nec cursus nec, rhoncus ac nisl. Aliquam ac mollis risus. Proin condimentum at orci vel dictum. " +
                    "Donec in faucibus ante, eget ullamcorper urna. Interdum et malesuada fames ac ante ipsum primis in faucibus. Morbi placerat porttitor auctor. " +
                    "Aliquam egestas non nisi eget convallis.\n\n" +
                    "Suspendisse ac risus hendrerit, dapibus lacus at, lacinia nulla. Mauris id congue purus, ac placerat arcu. Nulla convallis lacus vel feugiat posuere. " +
                    "Curabitur sem ipsum, rhoncus non est id, aliquam eleifend ligula. Cras quis dui eu diam feugiat vulputate eget non elit. Duis tempus congue ornare. " +
                    "Aliquam elit ligula, condimentum et urna sed, tincidunt viverra velit. Donec sagittis libero vitae arcu convallis molestie. " +
                    "Aenean a cursus urna, et efficitur lectus. Praesent ac sodales urna, vel eleifend lorem. Integer tortor sem, commodo ut augue vel, consectetur rhoncus ante. " +
                    "Nam vel lacus dui. Praesent pulvinar quis metus eget fringilla. Pellentesque dictum neque quis tellus consequat, sed consectetur dolor ornare."
            });
            NewsItems.Add(new NewsItem()
            {
                Title = "Test News Article 4",
                Author = "First Last",
                Date = "4/5/2020",
                Time = "1:41 PM",
                Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sit amet nibh id leo tempor feugiat et non tellus. " +
                    "Quisque turpis odio, dignissim et neque at, tristique condimentum odio. Sed fermentum nunc vitae fermentum facilisis. " +
                    "Nullam in massa fermentum, finibus sapien sit amet, eleifend urna. Cras eleifend rhoncus urna eu semper. " +
                    "Suspendisse tincidunt rutrum viverra. Fusce elit risus, vestibulum ac venenatis vel, facilisis vel arcu. " +
                    "In mollis posuere ipsum, ut condimentum sapien volutpat ac. Suspendisse eget cursus risus. Sed id ex nisi. " +
                    "Etiam eu ipsum non mi tristique blandit. Fusce lacinia dictum turpis, sit amet tristique dui congue a.\n\n" +
                    "Nam vestibulum vitae nibh id consectetur. Morbi iaculis eleifend fermentum. Cras quis libero nec dui feugiat commodo. " +
                    "Mauris diam felis, tincidunt nec cursus nec, rhoncus ac nisl. Aliquam ac mollis risus. Proin condimentum at orci vel dictum. " +
                    "Donec in faucibus ante, eget ullamcorper urna. Interdum et malesuada fames ac ante ipsum primis in faucibus. Morbi placerat porttitor auctor. " +
                    "Aliquam egestas non nisi eget convallis.\n\n" +
                    "Suspendisse ac risus hendrerit, dapibus lacus at, lacinia nulla. Mauris id congue purus, ac placerat arcu. Nulla convallis lacus vel feugiat posuere. " +
                    "Curabitur sem ipsum, rhoncus non est id, aliquam eleifend ligula. Cras quis dui eu diam feugiat vulputate eget non elit. Duis tempus congue ornare. " +
                    "Aliquam elit ligula, condimentum et urna sed, tincidunt viverra velit. Donec sagittis libero vitae arcu convallis molestie. " +
                    "Aenean a cursus urna, et efficitur lectus. Praesent ac sodales urna, vel eleifend lorem. Integer tortor sem, commodo ut augue vel, consectetur rhoncus ante. " +
                    "Nam vel lacus dui. Praesent pulvinar quis metus eget fringilla. Pellentesque dictum neque quis tellus consequat, sed consectetur dolor ornare."
            });
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }
}
