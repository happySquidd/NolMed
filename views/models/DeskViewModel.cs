using Microsoft.Identity.Client;
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

        public ObservableCollection<RoomOverviewBox> PatientRooms { get; set; }
        #endregion

        public DeskViewModel()
        {
            PatientRooms = new ObservableCollection<RoomOverviewBox> {};
            for (int i = 1; i < 51; i++)
            {
                RoomOverviewBox NewRoom = new RoomOverviewBox { RoomName = "Room " + i, BackgroundColor = "#3399ff" };
                PatientRooms.Add(NewRoom);
            }
            WelcomeMessage = "Welcome to the helpdesk!";
            AssignPatient = new RelayCommand(ButtonClicked, CanClick);
            RoomBoxClicked = new RelayCommand(RoomClicked);
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
                Debug.WriteLine($"Clicked: {clickedBox.RoomName}");
            }
        }
    }
}
