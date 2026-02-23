using NolMed.views.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class NavigationService
    {
        public static List<MenuItem> items = new List<MenuItem>
        {
            // TODO: change roles
            new MenuItem { Title = "Dashboard", Icon = null, RoleRequired = null, ViewTab = new DeskViewModel() },
            new MenuItem { Title = "Patient center", Icon = null, RoleRequired = null, ViewTab = new PatientViewModel() },
            new MenuItem { Title = "Patient vitals", Icon = null, RoleRequired = null, ViewTab = new PatientVitalsViewModel() },
            new MenuItem { Title = "Settings", Icon = null, RoleRequired = null, ViewTab = new SettingsViewModel() }
        };


        public static ObservableCollection<MenuItem> AllMenuItems = new ObservableCollection<MenuItem>();

        public static ObservableCollection<MenuItem> GetNavigationMenu(Employee user)
        {
            AllMenuItems.Clear();
            foreach (MenuItem item in items)
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
