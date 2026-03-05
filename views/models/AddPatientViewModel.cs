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
        private DateTime? _dob;
        public DateTime? DOB 
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
        

        public AddPatientViewModel()
        {
            BloodTypeSource = new List<string> { "Select", "AB+", "AB-", "A+", "A-", "B+", "B-", "O+", "O-"};
            BloodType = "Select";
            SubmitPatient = new RelayCommand(SubmitPatientFunc, CanSubmit);
        }

        public void SubmitPatientFunc(object sender)
        {
            Debug.WriteLine($"First name: {FirstName}, Last name: {LastName}, DOB: {DOB}");
        }

        public bool CanSubmit(object sender)
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && DOB.HasValue;
        }
    }
}
