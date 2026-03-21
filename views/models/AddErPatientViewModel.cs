using NolMed.database;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolMed.views.models
{
    public class AddErPatientViewModel : BaseView
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
        private DateOnly? _dob;
        public DateOnly? DOB
        {
            get => _dob;
            set { _dob = value; OnPropertyChanged(); }
        }
        private string _bloodType;
        public string BloodType
        {
            get => _bloodType;
            set { _bloodType = value; OnPropertyChanged(); }
        }
        public List<string> BloodTypeSource { get; set; }
        private string _severity;
        public string Severity
        {
            get => _severity;
            set { _severity = value; OnPropertyChanged(); }
        }
        public ICommand SubmitPatient { get; }
        public string _updateMessage;
        public string UpdateMessage
        {
            get => _updateMessage;
            set { _updateMessage = value; OnPropertyChanged(); }
        }
        #endregion

        public AddErPatientViewModel()
        {
            SubmitPatient = new RelayCommand(SubmitPatientFunc, CanSubmit);
        }

        public void SubmitPatientFunc(object sender)
        {
            // check if the patient already exists, if not register the new patient
            if (!DatabaseFunctions.PatientExists(FirstName, LastName, (DateOnly)DOB))
            {
                DatabaseFunctions.RegisterPatient(FirstName, LastName, (DateOnly)DOB, BloodType, "Yes");
                UpdateMessage = "Patient registered. ";
            }
            else
            {
                UpdateMessage = "Patient exists. ";
            }
            Patient patient = DatabaseFunctions.GetPatient(FirstName, LastName, (DateOnly)DOB);
            AssignRoom(patient);
        }

        public bool CanSubmit(object sender)
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || DOB == null || BloodType == "Select" || string.IsNullOrEmpty(Severity))
            {
                return false;
            }
            else
            {
                UpdateMessage = "";
                return true;
            }
        }

        public void AssignRoom(Patient patient)
        {
            var rooms = DatabaseFunctions.GetAllRooms();
            int roomNum = 0;
            // go through all the rooms and assign the patient to the first available room
            foreach (Room room in rooms)
            {
                if (room.RoomName == "Emergency room")
                {
                    if (room.PatientId == null)
                    {
                        roomNum = room.RoomNumber;
                        break;
                    }
                }
            }
            // if no rooms are available, add the patient to the queue
            if (roomNum == 0)
            {
                UpdateMessage += "No available rooms at the moment, added to queue.";
                return;
            }
            DatabaseFunctions.AssignPatientRoom(patient, roomNum);
            UpdateMessage += $"Patient assigned to room {roomNum}.";
        }
    }
}
