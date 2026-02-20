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

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        public DeskViewModel()
        {
            Message = "Welcome to the helpdesk!";
        }
    }
}
