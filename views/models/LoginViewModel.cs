using BC = BCrypt.Net;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using NolMed.database;

namespace NolMed.views.models
{
    public class LoginViewModel : BaseView
    {
        private readonly MainViewModel _mainViewModel;
        public string Username { get; set; }
        public ICommand LoginCommand { get; }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public LoginViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        public void ExecuteLogin(object button)
        {
            var passwordBox = button as PasswordBox;
            if (passwordBox == null) return;
            var password = BC.BCrypt.HashPassword(passwordBox.Password, Username);

            if (!DatabaseFunctions.FindUsername(Username)) { ErrorMessage = "Username doesn't exist"; return; }

            if (DatabaseFunctions.AuthenticateUser(password, Username))
            {
                var role = "Admin";
                var authenticatedUser = new Employee { Username = this.Username, Role = role };

                _mainViewModel.LoginUser(authenticatedUser);
            }
            else
            {
                ErrorMessage = "Wrong password";
            }

            passwordBox.Clear();

        }
    }
}
