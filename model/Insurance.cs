using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class Insurance
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
