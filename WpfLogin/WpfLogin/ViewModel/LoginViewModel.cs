using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfLogin.Views;
using static WpfLogin.LoginPageEnums;

namespace WpfLogin
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _username;
        private string _password;
        public DelegateCommand LoginCommand {  get; set; }

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(ValidateLogin);
        }

        private void ValidateLogin(object window)
        {
            if (string.Equals(Username, "root") && string.Equals(Password, "root"))
            {
                if (window is Window)
                {
                    var welcomeWindow = new WelcomeWindow(Username);
                    welcomeWindow.Show();

                    var loginWindow = (Window)window;
                    loginWindow.Close();
                }
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password");
            }
        }

        public LoginButtonState LoginButtonState
        {
            get
            {
                return (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) ?
                     LoginButtonState.Disabled : LoginButtonState.Enabled;
                
            }
        }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                onPropertyChanged(nameof(Username));
                onPropertyChanged(nameof(LoginButtonState));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                onPropertyChanged(nameof(Password));
                onPropertyChanged(nameof(LoginButtonState));
            }
        }

        private void onPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
