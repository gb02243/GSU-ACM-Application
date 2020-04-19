using GSUACM.ViewModels.ControlPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GSUACM.Views.Control_Panel.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendancePanelPage : ContentPage
    {
        public AttendancePanelPage()
        {
            InitializeComponent();
            BindingContext = new AttendancePanelViewModel(Navigation);
        }
    }
}