﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GSUACM.ViewModels
{
    public class PayDuesViewModel : INotifyPropertyChanged
    {
        public PayDuesViewModel()
        {   
            OpenPayPal = new Command(async () => await Browser.OpenAsync("https://www.paypal.com/us/home"));
            OpenCashApp = new Command(async () => await Browser.OpenAsync("https://cash.app"));
            OpenAmazon = new Command(async () => await Browser.OpenAsync("https://www.amazon.com"));
        }

        public ICommand OpenAmazon { get; }
        public ICommand OpenCashApp { get; }
        public ICommand OpenPayPal { get; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}

