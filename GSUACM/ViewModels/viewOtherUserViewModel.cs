using Xamarin.Forms;
using GSUACM.Models;
using GSUACM.Services;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Windows.Input;
using System.Data;

namespace GSUACM.ViewModels
{
    public class viewOtherUserViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public User SelectedUser { get; set; }
        public string PageTitle { get; set; }
        public string Email { get; set; }
        public string ClubPoints { get; set; }
        public string Title { get; set; }
        public string ProfileImage { get; set; }
        public ICommand back { get; set; }

        //tables for database columns
        private DataTable table = new DataTable();
        public viewOtherUserViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            back = new Command(CloseWindow);
            

            SelectedUser = GlobalVars.SelectedUser;

            PageTitle = SelectedUser.fullName;

            GetUserInfo();
            Email = SelectedUser.email;
            ClubPoints = SelectedUser.ClubPoints;
            ProfileImage = SelectedUser.ProfileImage;
            Title = SelectedUser.fullName;

            
            
        }

        private async void GetUserInfo()
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
                MySqlCommand command = new MySqlCommand("Select * from user where userID = @userID", db.getConnection());
                command.Parameters.Add("@userID", MySqlDbType.VarChar).Value = SelectedUser.userID;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                
                db.openConnection();
                adapter.SelectCommand = command;

                adapter.Fill(table);

                SelectedUser.email = table.Rows[0]["email"].ToString();
                SelectedUser.ClubPoints = table.Rows[0]["points"].ToString();
            }
            db.closeConnection();
            MessagingCenter.Send(this, "title");
        }

        private async void CloseWindow(object obj)
        {
            GlobalVars.SelectedUser = null;
            await Navigation.PopModalAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

