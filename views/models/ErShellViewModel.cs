using NolMed.model;
using NolMed.views.usercontrol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolMed.views.models
{
    public class ErShellViewModel : BaseView
    {
        public ERViewModel Section { get; }

        private object _currentSubView;
        public object CurrentSubView
        {
            get => _currentSubView;
            set { _currentSubView = value; OnPropertyChanged(); }
        }

        public ErShellViewModel()
        {
            Section = new ERViewModel();
            Section.NavigationRequested += roomNum =>
            {
                CurrentSubView = new PatientVitalsViewModel(roomNum);
            };
        }

        
    }
}
