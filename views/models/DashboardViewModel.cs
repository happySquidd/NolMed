using NolMed.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.views.models
{
    class DashboardViewModel : BaseView
    {
        private model.MenuItem _selectedItem;
        public model.MenuItem SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); CurrentView = _selectedItem.ViewTab; }
        }

        // the current view
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public List<MenuItem> AllMenuItems { get; set; }
        public List<MenuItem> dashboardItems = new List<MenuItem>
        {
            new MenuItem { Title="All rooms view", Icon=null, RoleRequired=null, ViewTab = new DeskViewModel() },
            new MenuItem { Title="Check in a patient", Icon=null, RoleRequired=null, ViewTab = new DeskViewModel() },
            new MenuItem { Title="Find patient", Icon=null, RoleRequired=null, ViewTab = new DeskViewModel() }
        };

        public DashboardViewModel() 
        {
            AllMenuItems = dashboardItems;
            OnPropertyChanged(nameof(AllMenuItems));
        }
    }
}
