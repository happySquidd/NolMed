using NolMed.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolMed.views.models
{
    public class DeskViewModel : BaseView
    {
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        private string _message;
        public string WelcomeMessage
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }
        public ICommand AssignPatient { get; }

        public DeskViewModel()
        {
            WelcomeMessage = "Welcome to the helpdesk!";
            AssignPatient = new RelayCommand(ButtonClicked);
        }

        public void ButtonClicked(object button)
        {
            Debug.WriteLine($"{FirstName}, {LastName}");
        }
    }
}
