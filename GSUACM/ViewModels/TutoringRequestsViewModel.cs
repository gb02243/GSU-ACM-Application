using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels
{
    class TutoringRequestsViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand AddClassCommand { get; set; }
        public ObservableCollection<> Employees { get { return employees; } }
        private DataTable table = new DataTable();
        public  TutoringRequestsViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
