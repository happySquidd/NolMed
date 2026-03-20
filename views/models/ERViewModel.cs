using Microsoft.Identity.Client;
using NolMed.database;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolMed.views.models
{
    public class ERViewModel : BaseView
    {
        public ICommand RoomBoxClicked { get; }
        public List<ErOverviewBox> EmergencyRooms { get; set; }
        public ERViewModel()
        {
            EmergencyRooms = new List<ErOverviewBox>();
            LoadRooms();
        }

        public void LoadRooms()
        {
            EmergencyRooms = DatabaseFunctions.GetEmergencyRoomsInfo();
            OnPropertyChanged(nameof(EmergencyRooms));
        }
    }
}
