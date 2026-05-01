using Microsoft.Identity.Client;
using NolMed.database;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolMed.views.models
{
    public class ERViewModel : BaseView
    {
        public List<ErOverviewBox> EmergencyRooms { get; set; }
        public ICommand RoomBoxClicked { get; }
        public event Action<ErOverviewBox> NavigationRequested;
        public ICommand RefreshPage { get; }

        public ERViewModel()
        {
            RoomBoxClicked = new RelayCommand(RoomClicked);
            EmergencyRooms = new List<ErOverviewBox>();
            LoadRooms();
            RefreshPage = new RelayCommand(Refresh);
        }
        
        public void LoadRooms()
        {
            EmergencyRooms = DatabaseFunctions.GetEmergencyRoomsInfo();
            foreach (var room in EmergencyRooms)
            {
                if (room.PatientName == null)
                {
                    room.BackgroundColor = "#84E858";
                }
                else
                {
                    room.BackgroundColor = "#279CF5";
                }
            }
            OnPropertyChanged(nameof(EmergencyRooms));
        }

        public void RoomClicked(object sender)
        {
            if (sender is ErOverviewBox box)
            {
                if (box.PatientName != null)
                {
                    NavigationRequested?.Invoke(box);
                }
                // debug
                //NavigationRequested?.Invoke(1);
            }
        }

        private void Refresh(object sender)
        {
            LoadRooms();
        }
    }
}
