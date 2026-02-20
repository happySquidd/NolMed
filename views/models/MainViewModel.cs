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

        public Staff CurrentUser { get; private set; }
        public ObservableCollection<model.MenuItem> MenuItems { get; set; }

        public MainViewModel()
        {
            MenuItems = new ObservableCollection<model.MenuItem>();
            IsLoggedIn = false;
            CurrentView = new LoginViewModel(this);
        }

        public void LoginUser(Staff User)
        {
            CurrentUser = User;
            IsLoggedIn = true;
            GeneratePage();
        }

        private void GeneratePage()
        {
            CurrentView = new DeskViewModel();
        }
    }
}
