using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ChatViewModels
{
    class ChatPageViewModel
    {
        public INavigation Navigation { get; set; }
        public string FullName { get; set; }
        public ICommand BackCommand { get; private set; }
        public string Chat_ID { get; private set; }
        public ChatPageViewModel(INavigation navigation, string chat_id)
        {
            this.Navigation = navigation;

            BackCommand = new Command(CloseModal);

            this.Chat_ID = chat_id;

            // TODO: Connect to database
            FullName = "Person "+Chat_ID;
        }

        public void CloseModal()
        {
            Navigation.PopModalAsync();
        }
    }
}
