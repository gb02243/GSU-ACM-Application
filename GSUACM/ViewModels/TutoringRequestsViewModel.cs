using GSUACM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
namespace GSUACM.ViewModels
{
    class TutoringRequestsViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand AddTutorCommand { get; set; }
        public ObservableCollection<Request> request { get; set; }
        private DataTable table = new DataTable();
        private DataTable table2 = new DataTable();
        private Request selectedRequest { get; set; }
        public  TutoringRequestsViewModel(INavigation navigation)
        {
            
            this.Navigation = navigation;
            this.request = Services.GlobalVars.request;
            this.AddTutorCommand = new Command(this.AddTutor);

        }
        Request  _yourSelectedItem;
        public Request YourSelectedItem
        {
            get
            {
                return _yourSelectedItem;
            }

            set
            {
                if (_yourSelectedItem != value)
                {
                    _yourSelectedItem = value;
                    AddTutor();
                }
            }
        }
        public async void AddTutor()
        {
            
            //Console.WriteLine(SelectedRequest.userid);
            var action =await Application.Current.MainPage.DisplayAlert("Do you want to tutor this person?", "tutoring", "Yes","No");
            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
            if (action)
            {

                DB db = new DB();
                //Console.WriteLine("Server"+db.openConnection());
                if (db.openConnection() == false)
                {
                    db.closeConnection();
                    await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
                }
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("select tutorID from tutor where userID= @userid ", db.getConnection());
                command.Parameters.Add("@userid", MySqlDbType.VarChar).Value = Services.GlobalVars.userid;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                MySqlCommand command3 = new MySqlCommand("select tutorID from tutorsession where sessionID=@sessionid2", db.getConnection());
                command3.Parameters.Add("@sessionid2", MySqlDbType.VarChar).Value = YourSelectedItem.sessionID;
                adapter3.SelectCommand = command3;
                adapter3.Fill(table2);
               //Console.WriteLine("The selected item " + table.Rows[0]["tutorID"]);
                if (table.Rows.Count == 0)
                {
                    db.closeConnection();
                    await Application.Current.MainPage.DisplayAlert("Tutor Error", "Only Tutors are allowed to Tutor", "Ok");
                }
                else
                {
                    if (table2.Rows.Count > 1)
                    {
                        db.closeConnection();
                        await Application.Current.MainPage.DisplayAlert("Tutor Error", "Tutor Already assigned", "Ok");
                    }
                    else
                    {
                        MySqlCommand command2 = new MySqlCommand("update tutorsession set tutorID=@tutorID where sessionID = @sessionID", db.getConnection());
                        command2.Parameters.Add("@tutorid", MySqlDbType.VarChar).Value = Convert.ToInt32(table.Rows[0]["tutorid"]);
                        Console.WriteLine("The selected item " + YourSelectedItem.sessionID);
                        command2.Parameters.Add("@sessionID", MySqlDbType.VarChar).Value = YourSelectedItem.sessionID;

                        if (command2.ExecuteNonQuery() == 1)
                        {
                            db.closeConnection();

                            await Application.Current.MainPage.DisplayAlert("You are now registered to Tutor", "Tutoring", "Ok");

                            // await Navigation.PopModalAsync();
                        }
                        else
                        {
                            db.closeConnection();
                            await Application.Current.MainPage.DisplayAlert("ERROR", "Failed to to Add Tutor Session", "Ok");

                        }
                    }
                }
            }
           
        }



        public event PropertyChangedEventHandler PropertyChanged;




    }
}
