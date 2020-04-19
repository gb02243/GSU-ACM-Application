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
    public partial class ViewAttendancePage : ContentPage
    {
        public ViewAttendancePage()
        {
            InitializeComponent();
            BindingContext = new ViewAttendanceViewModel(Navigation);
        }

        public ViewAttendancePage(Models.Attendance attendance)
        {
            InitializeComponent();
            BindingContext = new ViewAttendanceViewModel(Navigation, attendance);
        }
    }
}