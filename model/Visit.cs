using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class Visit
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }
}
