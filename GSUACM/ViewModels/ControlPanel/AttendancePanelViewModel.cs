using GSUACM.Models;
using GSUACM.Views.Control_Panel.Attendance;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GSUACM.ViewModels.ControlPanel
{
    class AttendancePanelViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SelectAttendanceCommand { get; set; }
        public ICommand PostAttendanceCommand { get; set; }
        public string EntryDate { get; set; }
        public string ResultDate { get; set; }
        public ObservableCollection<Attendance> AttendanceCollection { get; set; }
        public DataTable QueryResults { get; set; }
        public AttendancePanelViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            CloseWindowCommand = new Command(CloseWindow);
            SearchCommand = new Command(SearchDatabase);
            SelectAttendanceCommand = new Command<Attendance>(SelectAttendance);
            PostAttendanceCommand = new Command(PostAttendance);
            MessagingCenter.Subscribe<TakeAttendanceViewModel>(this, "attendance", (sender) =>
            {
                SearchDatabase();
            });
        }

        private void PostAttendance(object obj)
        {
            Navigation.PushModalAsync(new NavigationPage(new TakeAttendancePage()));
        }

        private void SelectAttendance(Attendance attendance)
        {
            Navigation.PushModalAsync(new NavigationPage(new ViewAttendancePage(attendance)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void SearchDatabase()
        {
            AttendanceCollection = new ObservableCollection<Attendance>();
            DB db = new DB();
            QueryResults = new DataTable();
            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(EntryDate))
                {
                    // create the adapter and query
                    MySqlCommand command = new MySqlCommand("SELECT date, body FROM attendance WHERE date LIKE @entrydate", db.getConnection());
                    command.Parameters.Add("@entrydate", MySqlDbType.VarChar).Value = EntryDate;
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    db.openConnection();
                    adapter.SelectCommand = command;

                    // set the adapter output
                    adapter.Fill(QueryResults);

                    if (QueryResults.Rows.Count > 0)
                    {
                        // convert the query result table into a list
                        for (int i = 0; i < QueryResults.Rows.Count; i++)
                        {
                            Attendance attendance = new Attendance()
                            {
                                Date = QueryResults.Rows[i]["date"].ToString(),
                                Body = QueryResults.Rows[i]["body"].ToString(),
                            };
                            AttendanceCollection.Add(attendance);
                            PropertyChanged(this, new PropertyChangedEventArgs("AttendanceCollection"));
                        }
                        db.closeConnection();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("No Results", "No attendance dates with that date were found.", "Ok");
                    }
                    db.closeConnection();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Oops!", "Please enter a date", "Ok");
                }
                db.closeConnection();
            }
            db.closeConnection();
        }

        private void CloseWindow()
        {
            Navigation.PopModalAsync();
        }
    }
}
