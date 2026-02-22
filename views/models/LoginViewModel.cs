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
using System.Diagnostics;

namespace NolMed.views.models
{
    public class LoginViewModel : BaseView
    {
        private readonly MainViewModel _mainViewModel;
        public string Username { get; set; }
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

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
            RegisterCommand = new RelayCommand(ExecuteRegister);
        }

        public void ExecuteLogin(object button)
        {
            // password handling
            var passwordBox = button as PasswordBox;
            if (string.IsNullOrEmpty(passwordBox?.Password)) { ErrorMessage = "Please enter a password"; return; }
            var password = passwordBox.Password;

            // find username
            if (!DatabaseFunctions.FindUsername(Username)) { ErrorMessage = "Username doesn't exist"; return; }

            // authenticate user
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

        public void ExecuteRegister(object button)
        {
            _mainViewModel.PromptRegister();
        }
    }
}
