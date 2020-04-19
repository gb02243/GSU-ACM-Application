using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ControlPanel
{
    class ViewAttendanceViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public Attendance Attendance { get; set; }
        public string AttendanceDate { get; set; }
        public string AttendanceBody { get; set; }
        public ViewAttendanceViewModel(INavigation navigation)
        {
            MissingAttendance();
        }
        public ViewAttendanceViewModel(INavigation navigation, Attendance attendance)
        {
            this.Navigation = navigation;
            this.Attendance = attendance;
            this.AttendanceBody = Attendance.Body;
            this.AttendanceDate = Attendance.Date;
            CloseWindowCommand = new Command(CloseWindow);
        }

        private void CloseWindow()
        {
            Navigation.PopModalAsync();
        }

        private async void MissingAttendance()
        {
            await Application.Current.MainPage.DisplayAlert("Server Error", "Attendance Not Found", "Ok");
            await Navigation.PopModalAsync();
        }
    }
}
