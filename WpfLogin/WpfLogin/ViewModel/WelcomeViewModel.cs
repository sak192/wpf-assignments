using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfLogin
{
    public class WelcomeViewModel
    {
        private string _username;

        public DelegateCommand WelcomeCommand { get; set; }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
            }
        }
        public WelcomeViewModel(string username)
        {
            Username = username;
            WelcomeCommand = new DelegateCommand(LogoutPage);
        }

        private void LogoutPage(object obj)
        {
            if(obj is Window)
            {
                var loginWindow = new MainWindow();
                loginWindow.Show();

                var logoutWindow = (Window)obj;
                logoutWindow.Close();
            }
        }
    }
}
