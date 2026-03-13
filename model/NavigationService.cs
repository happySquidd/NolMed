using NolMed.views.models;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NolMed.model
{
    public class NavigationService
    {
        public static ObservableCollection<MenuItem> AllMenuItems = new ObservableCollection<MenuItem>();
        public static ObservableCollection<MenuItem> DashboardMenu = new ObservableCollection<MenuItem>();

        public static List<MenuItem> mainItems = new List<MenuItem>
        {
            new MenuItem { Title = "Dashboard", Icon = null, RoleRequired = null, ViewTab = new DashboardViewModel() },
            new MenuItem { Title = "Patient center", Icon = null, RoleRequired = null, ViewTab = new PatientViewModel() },
            new MenuItem { Title = "ER center", Icon = null, RoleRequired = null, ViewTab = new ERViewModel() },
            new MenuItem { Title = "Settings", Icon = null, RoleRequired = null, ViewTab = new SettingsViewModel() }
        };


        public static ObservableCollection<MenuItem> GetNavigationMenu(Employee user)
        {
            AllMenuItems.Clear();
            foreach (MenuItem item in mainItems)
            {
                // TODO: create a proper role check
                if (true)
                {
                    AllMenuItems.Add(item);
                }
            }
            return AllMenuItems;
        }
    }
}
