using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ControlPanel
{
    class TakeAttendanceViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand TakeAttendanceCommand { get; set; }
        public string CurrentDate { get; set; }
        public string AttendanceBody { get; set; }
        public TakeAttendanceViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            CloseWindowCommand = new Command(CloseWindow);
            TakeAttendanceCommand = new Command(TakeAttendance);
            CurrentDate = DateTime.Now.ToString().Substring(0, 9);
        }

        private async void TakeAttendance()
        {
            DB db = new DB();
            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                // create the adapter and query
                MySqlCommand command = new MySqlCommand("INSERT INTO attendance(date, body) VALUES (@date, @body)", db.getConnection());
                command.Parameters.Add("@date", MySqlDbType.VarChar).Value = CurrentDate;
                command.Parameters.Add("@body", MySqlDbType.VarChar).Value = AttendanceBody;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                db.openConnection();
                adapter.SelectCommand = command;

                if (command.ExecuteNonQuery() == 1)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    await Application.Current.MainPage.DisplayAlert("Thank you", "Attendance Updated", "Ok");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
                db.closeConnection();
            }
            db.closeConnection();
            MessagingCenter.Send(this, "sttendance");
        }

        private void CloseWindow()
        {
            Navigation.PopModalAsync();
        }
    }
}
