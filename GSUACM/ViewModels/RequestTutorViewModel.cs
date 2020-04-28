using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GSUACM.Models;

namespace GSUACM.ViewModels
{
 
    class RequestTutorViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand AddTutoringSessionCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public DateTime date { get; set; }
        private ObservableCollection<Request> request;
        public string subject { get; set; }
        private DataTable table = new DataTable();
        private DataTable table2 = new DataTable();
        public event PropertyChangedEventHandler PropertyChanged;

        public RequestTutorViewModel(INavigation navigation)
        {
            
            this.Navigation = navigation;
            this.AddTutoringSessionCommand = new Command(this.addTutoringSession);
            this.CancelCommand = new Command(this.buttonCancel_Click);
        }
        public async void addTutoringSession()
        {
            DB db = new DB();
            if (db.openConnection() == false)
            {
                db.closeConnection();
               await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
            DateTime Date = date;
            String Subject = subject ;


            MySqlCommand command = new MySqlCommand("Insert into tutorsession(subject,date,userID) values(@subject,@date,@userid)", db.getConnection());
            command.Parameters.Add("@userid", MySqlDbType.VarChar).Value = Services.GlobalVars.userid;
         
            command.Parameters.Add("@subject", MySqlDbType.VarChar).Value = subject;
            command.Parameters.Add("@date", MySqlDbType.DateTime).Value = date;

         

            db.openConnection();
            if (checkTextBoxesValues() == true)
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MySqlCommand command2 = new MySqlCommand("select sessionID from tutorsession where userID = @userid2", db.getConnection());
                    command2.Parameters.Add("@userid2", MySqlDbType.VarChar).Value = Services.GlobalVars.userid;
                    adapter.SelectCommand = command2;
                    adapter.Fill(table);
                    MySqlCommand command3 = new MySqlCommand("select * from user where userID = @userid3", db.getConnection());
                    command3.Parameters.Add("@userid3", MySqlDbType.VarChar).Value = Services.GlobalVars.userid;
                    adapter2.SelectCommand = command3;
                    adapter2.Fill(table2);
                    Services.GlobalVars.request.Add(new Request() { sessionID = Convert.ToInt32(table.Rows[0]["sessionID"]) , subject = subject , Date = Convert.ToString(date) , userid = Services.GlobalVars.userid,username="User: "+ table2.Rows[0]["fname"].ToString()+" "+ table2.Rows[0]["lname"].ToString() });
                    db.closeConnection();
                    await Application.Current.MainPage.DisplayAlert("Tutor Session Added", "Succesfully Added as Tutor Session", "Ok");

                   // await Navigation.PopModalAsync();
                }
                else
                {
                    db.closeConnection();
                    await Application.Current.MainPage.DisplayAlert("ERROR", "Failed to to Add Tutor Session", "Ok");

                }
            }
            else
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Information Left Blank", "Please Fill In Info", "Ok");

            }
        }

        public Boolean checkTextBoxesValues()
        {

            if (String.IsNullOrEmpty(subject))
            {
                Console.WriteLine("The email is " + GSUACM.Services.GlobalVars.email);
                return false;
            }
            if (date == DateTime.MinValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private async void buttonCancel_Click()
        {
            await this.Navigation.PopModalAsync();
        }
    }
}
