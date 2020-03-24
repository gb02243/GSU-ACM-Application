
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Input;
using Xamarin.Forms;
namespace XF_Login.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        //string conStr = ConfigurationManager.ConnectionStrings["MembersConnectionString"].ConnectionString;
        string sql = "SELECT * FROM Members where Username = @user and Password = @pass";
       
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        private string phoneNum;
        public string PhoneNum
        {
            get { return password; }
            set
            {
                phoneNum = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public void OnSubmit()
        {
            if (email != "macoratti@yahoo.com" || password != "secret")
            {
                DisplayInvalidLoginPrompt();
            }
        }
    }
}
