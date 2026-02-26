using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.views.models
{
    public class PatientVitalsViewModel : BaseView
    {
        private string _placeholderText;

        public string PlaceholderText
        {
            get => _placeholderText;
            set { _placeholderText = value; OnPropertyChanged(); }
        }

        public PatientVitalsViewModel()
        {
            PlaceholderText = "Patient Vitals Tab";
        }
    }
}
