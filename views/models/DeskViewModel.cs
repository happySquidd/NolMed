using Microsoft.Identity.Client;
using NolMed.database;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NolMed.views.models
{
    public class DeskViewModel : BaseView
    {
        #region Definitions
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        private string _message;
        public string WelcomeMessage
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }
        public ICommand AssignPatient { get; }
        public ICommand RoomBoxClicked { get; }

        public List<RoomOverviewBox> PatientRooms { get; set; }
        public List<Room> AllRooms { get; set; }
        #endregion

        public DeskViewModel()
        {
            PatientRooms = new List<RoomOverviewBox> {};
            PopulateRooms();
            
            WelcomeMessage = "View of room availability";
            AssignPatient = new RelayCommand(ButtonClicked, CanClick);
            RoomBoxClicked = new RelayCommand(RoomClicked);
        }

        public void PopulateRooms()
        {
            AllRooms = DatabaseFunctions.GetAllRooms();
            foreach (Room room in AllRooms)
            {
                RoomOverviewBox roomOverview = new()
                {
                    RoomNumber = room.RoomNumber,
                    RoomName = room.RoomName,
                    // change this later
                    PatientName = room.PatientId.ToString()
                };
                if (room.PatientId != null)
                {
                    roomOverview.BackgroundColor = "#279CF5";
                }
                else
                {
                    roomOverview.BackgroundColor = "#6CE66A";
                }
                PatientRooms.Add(roomOverview);
            }
            ;
        }

        public void ButtonClicked(object button)
        {
            Debug.WriteLine($"{FirstName}, {LastName}");
        }

        public bool CanClick(object sender)
        {
            return !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(FirstName);
        }

        public void RoomClicked(object sender)
        {
            if (sender is RoomOverviewBox clickedBox)
            {
                Debug.WriteLine($"Clicked: {clickedBox.RoomNumber}");
            }
        }
    }
}
