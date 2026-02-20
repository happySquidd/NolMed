using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Mrn { get; set; }
        public DateOnly Dob { get; set; }
        public string Blood { get; set; }
        public bool InPatient { get; set; }
    }
}
