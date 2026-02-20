using NolMed.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NolMed.views.models
{
    public class LoginViewModel
    {
        private readonly MainViewModel _mainViewModel;
        public string Username { get; set; }
        public ICommand LoginCommand { get; }


        public LoginViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        public void ExecuteLogin(object button)
        {
            var passwordBox = button as PasswordBox;
            if (passwordBox == null) return;
            // 2. Safely extract the password 
            string plainTextPassword = passwordBox.Password;

            var role = "Admin";
            var authenticatedUser = new Staff
            {
                Username = this.Username,
                Role = role,
            };

            _mainViewModel.LoginUser(authenticatedUser);
        }
    }
}
