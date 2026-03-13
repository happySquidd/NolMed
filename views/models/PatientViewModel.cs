using NolMed.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            set { _selectedRoom = value; OnPropertyChanged(); }
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
        #endregion

        public PatientViewModel()
        {
            WelcomeText = "Patient info tab";
            IsPopupVisible = "Hidden";
            
        }
    }
}
