using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Input;
using GSUACM.ViewModels;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using System.Diagnostics;
using System.Data;
using GSUACM.Services;
using GSUACM;
using GSUACM.Views;

namespace GSUACM.ViewModels
{
    public class pickPictureViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand shark { get; }
        public ICommand turtle { get; set; }
        public ICommand dog { get; set; }
        public ICommand lamb { get; set; }
        public ICommand mouse { get; set; }
        public ICommand penquin { get; set; }
        public ICommand ladybug { get; set; }
        public ICommand elephant { get; set; }
        public String picture;
        public pickPictureViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.shark = new Command(this.SharkRN);
            this.turtle = new Command(this.TurtleRN);
            this.dog = new Command(this.DogRN);
            this.lamb = new Command(this.LambRN);
            this.mouse = new Command(this.MouseRN);
            this.penquin = new Command(this.PenquinRN);
            this.ladybug = new Command(this.LadybugRN);
            this.elephant = new Command(this.ElephantRN);


        }

        public void TurtleRN()
        {
            this.picture = "turtle.png";
            UpdateProfileImage();
        }
        public void SharkRN()
        {
            this.picture = "shark.png";
            UpdateProfileImage();
        }
        public void DogRN()
        {
            this.picture = "dog.jpg";
            UpdateProfileImage();
        }
        public void LambRN()
        {
            this.picture = "lamb.png";
            UpdateProfileImage();
        }
        public void MouseRN()
        {
            this.picture = "rat.png";
            UpdateProfileImage();
        }
        public void PenquinRN()
        {
            this.picture = "penquin.png";
            UpdateProfileImage();
        }
        public void LadybugRN()
        {
            this.picture = "ladybug.png";
            UpdateProfileImage();
        }
        public void ElephantRN()
        {
            this.picture = "elephant.png";
            UpdateProfileImage();
        }
        private async void UpdateProfileImage()
        {
            DB db = new DB();

            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("update user set image = @image where userID = @userid", db.getConnection());
                command.Parameters.Add("@image", MySqlDbType.VarChar).Value = picture;
                command.Parameters.Add("@userid", MySqlDbType.VarChar).Value = GlobalVars.User.userID;
                db.openConnection();
                adapter.SelectCommand = command;

                command.ExecuteNonQuery();
                GlobalVars.User.ProfileImage = picture;
                if (Application.Current.Properties.ContainsKey("UserProfileImg"))
                    Application.Current.Properties.Remove("UserProfileImg");
                Application.Current.Properties.Add("UserProfileImg", GlobalVars.User.ProfileImage);

                db.closeConnection();

                MessagingCenter.Send<pickPictureViewModel>(this, "update");
                await Application.Current.MainPage.DisplayAlert("Your is profile picture is changed", "Account Updated", "Ok");
                await Navigation.PopModalAsync();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

