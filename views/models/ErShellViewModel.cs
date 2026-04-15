using NolMed.model;
using NolMed.views.usercontrol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolMed.views.models
{
    public class ErShellViewModel : BaseView
    {
        public ERViewModel Section { get; }

        public ICommand RoomBoxClicked { get; }

        private object _currentSubView;
        public object CurrentSubView
        {
            get => _currentSubView;
            set { _currentSubView = value; OnPropertyChanged(); }
        }

        public ErShellViewModel()
        {
            Section = new ERViewModel();
            RoomBoxClicked = new RelayCommand(RoomClicked);
        }

        public void RoomClicked(object sender)
        {
            if (sender is ErOverviewBox box)
            {
                if (box.PatientName != null)
                {
                    CurrentSubView = new PatientVitalsViewModel(box.RoomNumber);
                }
            }
        }
    }
}
