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
    class HomePageViewModel : INotifyPropertyChanged
    {
        public DataTable table { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ClubPoints { get; set; }
        
        public ImageSource someImage { get; set; }

        public INavigation Navigation { get; set; }
        public ICommand CreateProfileCommand { get; set; }
        public ICommand EditProfileCommand { get; set; }
        public ICommand BecomeTutorCommand { get; set; }


        public HomePageViewModel(INavigation navigation)
        {

            String pathName = "local:ImageResource GSUACM.ProfileImage.";
            this.Navigation = navigation;
            
            MessagingCenter.Subscribe<LoginViewModel, string>(this, "Hi", (sender, arg) => {
                Name = Services.GlobalVars.User.fname + " " + Services.GlobalVars.User.lname;
                Email = Services.GlobalVars.User.email;
                Phone = Services.GlobalVars.User.phone;
                ClubPoints = Services.GlobalVars.User.ClubPoints;
                this.someImage = pathName + GlobalVars.User.ProfileImage;
                
                

            });
            MessagingCenter.Subscribe<EditProfileViewModel, string>(this, "Hi", (sender, arg) => {
                Name = Services.GlobalVars.User.fname + " " + Services.GlobalVars.User.lname;
                Email = Services.GlobalVars.User.email;
                Phone = Services.GlobalVars.User.phone;
                ClubPoints = Services.GlobalVars.User.ClubPoints;
                this.someImage = pathName + GlobalVars.User.ProfileImage;

                Console.WriteLine(someImage.ToString());
            });
            
            
            
            Name = Services.GlobalVars.User.fname + " " + Services.GlobalVars.User.lname;
            Email = Services.GlobalVars.User.email;
            Phone = Services.GlobalVars.User.phone;
            ClubPoints = Services.GlobalVars.User.ClubPoints;
            this.someImage = pathName + GlobalVars.User.ProfileImage;

            this.CreateProfileCommand = new Command(this.buttonCreateProfile_Click);
            this.EditProfileCommand = new Command(this.buttonEditProfile_Click);
            this.BecomeTutorCommand = new Command(this.buttonBecomeTutor_Click);
            
            Console.WriteLine(someImage.ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void buttonCreateProfile_Click()
        {
            await Navigation.PushAsync(new createProfile());
        }
        private async void buttonEditProfile_Click()
        {
            await Navigation.PushModalAsync(new editProfile());
        }
        private async void buttonBecomeTutor_Click()
        {
            await Navigation.PushModalAsync(new BecomeTutor());
        }

        /** public async void profileImage()
        {
            DB db = new DB();

            if (db.openConnection() == false)
            {
                db.closeConnection();
                await Application.Current.MainPage.DisplayAlert("Server Error", "Try Again Later", "Ok");
            }
            else
            {
                String pathname = "local:ImageResource GSUACM.ProfileImages.";
                // create the adapter and query
                MySqlCommand command = new MySqlCommand("SELECT image from user where userID = @userID", db.getConnection());

                command.Parameters.Add("@userID", MySqlDbType.VarChar).Value = GlobalVars.User.userID;

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                db.openConnection();
                adapter.SelectCommand = command;

                // set the adapter output
                adapter.Fill(table);

                
                    
                    pathname = pathname + table.Rows[0]["image"].ToString();
                    GlobalVars.User.profileImage = pathname;
                    MessagingCenter.Send<HomePageViewModel>(this, "change");
              
                db.closeConnection();
            }
                        

        }*/
    }

}

