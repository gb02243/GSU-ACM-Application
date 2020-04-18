using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Input;
using System.Data;

namespace GSUACM.ViewModels
{
    class BecomeTutorViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand AddClassCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string courseTitle { get; set; }
        public string userID { get; set; }

        public string courseCode { get; set; }
        private DataTable table = new DataTable();
        public BecomeTutorViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.AddClassCommand = new Command(this.AddClassToTutor);
            this.CancelCommand = new Command(this.buttonCancel_Click);
        }

        // label go to signup CLICK
        private async void AddClassToTutor()
        {
            
            DB db = new DB();
            if (db.openConnection() == false)
            {
                db.closeConnection();
               await  Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            String userid = userID;
            String coursetitle = courseTitle;
            String coursecode = courseCode;
            
            MySqlCommand command3 = new MySqlCommand("select userID from tutor where userID = @userid4", db.getConnection());
            command3.Parameters.Add("@userid4", MySqlDbType.VarChar).Value = Services.GlobalVars.userid;
            
            adapter.SelectCommand = command3;
            adapter.Fill(table);
            Console.WriteLine("The email is " + checkTextBoxesValues());
            db.openConnection();
            if (table.Rows.Count == 0)
            {
                MySqlCommand command = new MySqlCommand("Insert into tutor(userID) values(@userid)", db.getConnection());
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = Services.GlobalVars.email;
                command.Parameters.Add("@userid", MySqlDbType.VarChar).Value = Services.GlobalVars.userid;
                MySqlCommand command2 = new MySqlCommand("insert into course(courseCode,courseTitle,tutorID) values(@coursecode,@coursetitle,@userid2)", db.getConnection());
                command2.Parameters.Add("@userid2", MySqlDbType.VarChar).Value = Services.GlobalVars.userid;
                command2.Parameters.Add("@coursecode", MySqlDbType.VarChar).Value = courseCode;
                command2.Parameters.Add("@coursetitle", MySqlDbType.VarChar).Value = courseTitle;
                db.openConnection();
                if (checkTextBoxesValues() == true)
                {
                    if (command.ExecuteNonQuery() == 1 && command2.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();
                        await Application.Current.MainPage.DisplayAlert("Tutor Added", "Succesfully Added as Tutor", "Ok");
                       
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        db.closeConnection();
                        await Application.Current.MainPage.DisplayAlert("ERROR", "Failed to become tutor", "Ok");
                        

                    }
                }
                else
                {
                    db.closeConnection();
                    await Application.Current.MainPage.DisplayAlert("Information Left Blank", "Please Fill In Info", "Ok");
                    
                }
            }
            else
            {
                MySqlCommand command4 = new MySqlCommand("insert into course(courseCode,courseTitle,tutorID) values(@coursecode,@coursetitle,@userid3)", db.getConnection());
                command4.Parameters.Add("@userid3", MySqlDbType.VarChar).Value = Services.GlobalVars.userid;
                command4.Parameters.Add("@coursecode", MySqlDbType.VarChar).Value = courseCode;
                command4.Parameters.Add("@coursetitle", MySqlDbType.VarChar).Value = courseTitle;
                db.openConnection();
                if (checkTextBoxesValues() == true)
                {
                    if (command4.ExecuteNonQuery() == 1)
                    {
                        db.closeConnection();

                        await Application.Current.MainPage.DisplayAlert("Tutor Added", "Succesfully Added as Tutor", "Ok");
                       
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        db.closeConnection();
                        await Application.Current.MainPage.DisplayAlert("ERROR", "Failed to become tutor", "Ok");
                        
                    }
                }
                else
                {
                    db.closeConnection();
                    await Application.Current.MainPage.DisplayAlert("Information Left Blank", "Please Fill In Info", "Ok");
                    
                }
            }
            db.closeConnection();
        }
        public Boolean checkTextBoxesValues()
        {

            if (String.IsNullOrEmpty(Services.GlobalVars.email))
            {
                Console.WriteLine("The email is "+ GSUACM.Services.GlobalVars.email);
                return false;
            }
            if (String.IsNullOrEmpty(courseCode))
            {
                return false;
            }
            if (String.IsNullOrEmpty(courseTitle))
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
        public event PropertyChangedEventHandler PropertyChanged;
    }
        
    }

