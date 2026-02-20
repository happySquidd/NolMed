using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class Vitals
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public decimal Temperature { get; set; }
        public int Bpm { get; set; }
        public DateTime Time { get; set; }
    }
}
