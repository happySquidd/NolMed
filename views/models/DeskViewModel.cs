using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.views.models
{
    public class DeskViewModel : BaseView
    {
        private string _message;

        public string WelcomeMessage
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        public DeskViewModel()
        {
            WelcomeMessage = "Welcome to the helpdesk!";
        }
    }
}
