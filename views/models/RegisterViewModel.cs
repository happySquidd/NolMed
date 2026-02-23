using NolMed.database;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using BC = BCrypt.Net;

namespace NolMed.views.models
{
    public class RegisterViewModel : BaseView
    {
        private readonly MainViewModel _mainViewModel;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public RegisterViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            RegisterCommand = new RelayCommand(RegisterMe);
            LoginCommand = new RelayCommand(BackToLogin);
        }

        private void RegisterMe(object button)
        {
            // validate inputs
            if (!ValidateInputs()) { return; }
            // password functions
            var passwordBox = button as PasswordBox;
            if (string.IsNullOrEmpty(passwordBox?.Password)) { ErrorMessage = "Please enter a password"; return; }
            var password = BC.BCrypt.HashPassword(passwordBox.Password);

            // check if username exists. username must be unique
            if (DatabaseFunctions.FindUsername(Username)) { ErrorMessage = "Username exists, please try again."; return; }

            // create employee object
            Employee registeredUser = new Employee() { FirstName = FirstName, LastName = LastName, Username = Username, Password = password };

            // pass new employee to database
            DatabaseFunctions.RegisterUser(registeredUser);
            // login new user
            _mainViewModel.LoginUser(registeredUser);

            passwordBox.Clear();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                ErrorMessage = "Please enter first and last name";
                return false;
            }
            if (string.IsNullOrEmpty(Username))
            {
                ErrorMessage = "Please enter a username";
                return false;
            }
            return true;
        }

        private void BackToLogin(object button)
        {
            _mainViewModel.PromptLogin();
        }
    }
}
