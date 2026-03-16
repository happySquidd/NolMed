using NolMed.database;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace NolMed.views.models
{
    public class AddPatientViewModel : BaseView
    {
        private string _firstName;
        public string FirstName 
        {
            get => _firstName;
            set{ _firstName = value; OnPropertyChanged(); } 
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
        public List<string> BloodTypeSource { get; set; }
        private string _bloodType;
        public string BloodType 
        {
            get => _bloodType;
            set { _bloodType = value; OnPropertyChanged(); }
        }
        public ICommand SubmitPatient { get; }
        private string _updateMessage;
        public string UpdateMessage
        {
            get => _updateMessage;
            set { _updateMessage = value; OnPropertyChanged(); }
        }
        

        public AddPatientViewModel()
        {
            BloodTypeSource = new List<string> { "Select", "AB+", "AB-", "A+", "A-", "B+", "B-", "O+", "O-"};
            BloodType = "Select";
            SubmitPatient = new RelayCommand(SubmitPatientFunc, CanSubmit);
        }

        public void SubmitPatientFunc(object sender)
        {
            Debug.WriteLine($"First name: {FirstName}, Last name: {LastName}, DOB: {DOB}");
            // check if the patient already exists, if not register the new patient
            if (!DatabaseFunctions.PatientExists(FirstName, LastName, (DateOnly)DOB))
            {
                DatabaseFunctions.RegisterPatient(FirstName, LastName, (DateOnly)DOB);
                UpdateMessage = "Added the patient. ";
            } 
            else 
            { 
                UpdateMessage = "Patient exists. "; 
            }
            // replace with new function
            //Patient patient = DatabaseFunctions.FindPatient(LastName, (DateOnly)DOB);
            Patient patient = new Patient();
            AssignRoom(patient);
        }

        public bool CanSubmit(object sender)
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && DOB.HasValue;
        }

        public void AssignRoom(Patient patient)
        {
            int emptyRoom = 0;
            // assign an empty room to the patient
            List<Room> AllRooms = DatabaseFunctions.GetAllRooms();
            foreach (Room room in AllRooms)
            {
                if (room.PatientId == null)
                {
                    emptyRoom = room.RoomNumber;
                    break;
                }
            }
            DatabaseFunctions.AssignPatientRoom(patient, emptyRoom);
            UpdateMessage += $"Assigned room: {emptyRoom}";
        }
    }
}
