using NolMed.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NolMed.views.models
{
    public class MainViewModel : BaseView
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set { _isLoggedIn = value; OnPropertyChanged(); }
        }

        public Employee CurrentUser { get; private set; }
        public ObservableCollection<model.MenuItem> MenuItems { get; set; }

        public MainViewModel()
        {
            IsLoggedIn = false;
            PromptLogin();
        }

        public void LoginUser(Employee User)
        {
            CurrentUser = User;
            IsLoggedIn = true;
            GeneratePage();
        }

        public void PromptRegister()
        {
            CurrentView = new RegisterViewModel(this);
        }

        public void PromptLogin()
        {
            CurrentView = new LoginViewModel(this);
        }

        private void GeneratePage()
        {
            CurrentView = new DeskViewModel();

            MenuItems = NavigationService.GetNavigationMenu(CurrentUser);
            OnPropertyChanged(nameof(MenuItems));
        }
    }
}
