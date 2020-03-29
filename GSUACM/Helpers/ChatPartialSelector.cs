using GSUACM.Models.ChatModels;
using GSUACM.Views.Chat_Partials;
using Xamarin.Forms;

namespace GSUACM.Helpers
{
    class ChatPartialSelector : DataTemplateSelector
    {
        DataTemplate directDataTemplate;
        DataTemplate groupDataTemplate;

        public ChatPartialSelector()
        {
            this.directDataTemplate = new DataTemplate(typeof(DirectViewCell));
            this.groupDataTemplate = new DataTemplate(typeof(GroupViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageType = item as Chat;
            if (messageType == null)
                return null;


            return (messageType.IsGroup) ? groupDataTemplate : directDataTemplate;
        }
    }
}
