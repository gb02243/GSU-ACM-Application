using GSUACM.Models.ChatModels;
using GSUACM.Views.Chat.Cells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GSUACM.Helpers
{
    class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;

        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;
            if (messageVm == null)
                return null;

            // TODO: fix this for the new user class
            return (messageVm.alias == App.User.userID) ? incomingDataTemplate : outgoingDataTemplate;
        }
    }
}
