using NolMed.database;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolMed.views.models
{
    public class PatientViewModel : BaseView
    {
        #region Definitions
        private string _welcomeText;
        public string WelcomeText
        {
            get => _welcomeText;
            set { _welcomeText = value; OnPropertyChanged(); }
        }
        private string _selectedRoom;
        public string SelectedRoom
        {
            get => _selectedRoom;
            set { _selectedRoom = value; OnPropertyChanged(); UpdateRoomPatient(); }
        }
        public List<string> RoomPicker { get; set; }
        private string _isPopupVisible;
        public string IsPopupVisible 
        { 
            get=> _isPopupVisible;
            set { _isPopupVisible = value; OnPropertyChanged(); } 
        }
        private string _patientName;
        public string PatientName
        {
            get => _patientName;
            set { _patientName = value; OnPropertyChanged(); }
        }
        private string _patientDob;
        public string PatientDob
        {
            get => _patientDob;
            set { _patientDob = value; OnPropertyChanged(); }
        }
        private string _patientBloodType;
        public string BloodType
        {
            get => _patientBloodType;
            set { _patientBloodType = value; OnPropertyChanged(); }
        }
        private string _insuranceNumber;
        public string InsuranceNumber
        {
            get => _insuranceNumber;
            set { _insuranceNumber = value; OnPropertyChanged(); }
        }
        private string _insuranceName;
        public string InsuranceName
        {
            get => _insuranceName;
            set { _insuranceName = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Allergy> AllergiesList { get; set; }
        private string _streetName;
        public string StreetName
        {
            get => _streetName;
            set { _streetName = value; OnPropertyChanged(); }
        }
        private string _cityName;
        public string CityName
        {
            get => _cityName;
            set { _cityName = value; OnPropertyChanged(); }
        }
        private string _stateName;
        public string StateName
        {
            get => _stateName;
            set { _stateName = value; OnPropertyChanged(); }
        }
        private string _zipName;
        public string ZipName
        {
            get => _zipName;
            set { _zipName = value; OnPropertyChanged(); }
        }
        private string _countryName;
        public string CountryName
        {
            get => _countryName;
            set { _countryName = value; OnPropertyChanged(); }
        }
        public ICommand SaveInfo { get; }
        public Patient patient { get; set; }
        private List<Room> _allRooms { get; set; }
        #endregion

        public PatientViewModel()
        {
            WelcomeText = "Patient info tab";
            IsPopupVisible = "Hidden";
            SaveInfo = new RelayCommand(SaveInfoFunc, CanSave);
            PopulateRoomList();
        }

        public void PopulateRoomList()
        {
            _allRooms = DatabaseFunctions.GetAllRooms();
            CreateRoomList();
        }

        public void CreateRoomList()
        {
            RoomPicker = new List<string>();
            foreach (Room room in _allRooms)
            {
                if (room.PatientId == null) continue;
                string roomName = $"Room {room.RoomNumber}";
                RoomPicker.Add(roomName);
            }
        }

        public void UpdateRoomPatient()
        {
            // define patient parameters based on the selected room
            if (string.IsNullOrEmpty(SelectedRoom)) return;
            int roomNumber = Convert.ToInt32(SelectedRoom.Split(' ')[1]);
            Room selectedRoom = _allRooms.Find(r => r.RoomNumber == roomNumber);
            patient = DatabaseFunctions.FindPatientById((int)selectedRoom.PatientId);
            if (patient == null) return;
            // fill in patient info
            PatientName = patient.FirstName + " " + patient.LastName;
            PatientDob = patient.Dob.ToShortDateString();
            BloodType = patient.Blood;
            // find insurance
            var insurance = DatabaseFunctions.GetPatientInsurance(patient);
            if (insurance != null)
            {
                InsuranceNumber = insurance.Number.ToString();
                InsuranceName = insurance.Name;
            }
            else
            {
                InsuranceNumber = "";
                InsuranceName = "";
            }
            // find address
            var address = DatabaseFunctions.GetPatientBilling(patient);
            if (address != null) 
            { 
                StreetName = address.Street;
                CityName = address.City;
                StateName = address.State;
                ZipName = address.Zip.ToString();
                CountryName = address.Country;
            }
            else
            {
                StreetName = "";
                CityName = "";
                StateName = "";
                ZipName = "";
                CountryName = "";
            }
        }

        public void SaveInfoFunc(object sender)
        {
            if (patient == null) return;
            DatabaseFunctions.UpdatePatientInfo(patient, BloodType, Convert.ToInt32(InsuranceNumber), InsuranceName, StreetName, CityName, StateName, Convert.ToInt32(ZipName), CountryName);
        }

        public bool CanSave(object sender)
        {
            if (string.IsNullOrEmpty(PatientName) || 
                string.IsNullOrEmpty(PatientDob) || 
                //string.IsNullOrEmpty(BloodType) || 
                string.IsNullOrEmpty(InsuranceNumber) || 
                string.IsNullOrEmpty(InsuranceName) || 
                string.IsNullOrEmpty(StreetName) || 
                string.IsNullOrEmpty(CityName) || 
                string.IsNullOrEmpty(StateName) || 
                string.IsNullOrEmpty(ZipName) || 
                string.IsNullOrEmpty(CountryName))
            {
                return false;
            }
            return true;
        }
    }
}
