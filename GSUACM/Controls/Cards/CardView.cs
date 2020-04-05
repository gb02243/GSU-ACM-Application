using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GSUACM.Controls.Cards
{
    class CardView : ContentView
    {
        public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(nameof(CardTitle), typeof(string), typeof(CardView), string.Empty);
        public static readonly BindableProperty CardAuthorProperty = BindableProperty.Create(nameof(CardAuthor), typeof(string), typeof(CardView), string.Empty);
        public static readonly BindableProperty CardDateProperty = BindableProperty.Create(nameof(CardDate), typeof(string), typeof(CardView), string.Empty);
        public static readonly BindableProperty CardTimeProperty = BindableProperty.Create(nameof(CardTime), typeof(string), typeof(CardView), string.Empty);
        public static readonly BindableProperty CardBodyProperty = BindableProperty.Create(nameof(CardBody), typeof(string), typeof(CardView), string.Empty);


        public string CardTitle
        {
            get => (string)GetValue(CardTitleProperty);
            set => SetValue(CardTitleProperty, value);
        }
        public string CardAuthor
        {
            get => (string)GetValue(CardAuthorProperty);
            set => SetValue(CardAuthorProperty, value);
        }
        public string CardDate
        {
            get => (string)GetValue(CardDateProperty);
            set => SetValue(CardDateProperty, value);
        }
        public string CardTime
        {
            get => (string)GetValue(CardTimeProperty);
            set => SetValue(CardTimeProperty, value);
        }
        public string CardBody
        {
            get => (string)GetValue(CardBodyProperty);
            set => SetValue(CardBodyProperty, value);
        }
    }
}
