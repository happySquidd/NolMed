using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.views.models
{
    public class PatientViewModel : BaseView
    {
        private string _welcomeText;
        
        public string WelcomeText
        {
            get => _welcomeText;
            set { _welcomeText = value; OnPropertyChanged(); }
        }

        public PatientViewModel()
        {
            WelcomeText = "Patient info tab";
        }
    }
}
