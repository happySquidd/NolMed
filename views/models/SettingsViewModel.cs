using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.views.models
{
    public class SettingsViewModel : BaseView
    {
        public string _placeholderText;

        public string PlaceholderText
        {
            get { return _placeholderText; }
            set { _placeholderText = value; OnPropertyChanged(); }
        }

        public SettingsViewModel()
        {
            PlaceholderText = "Settings tab";
        }
    }
}
