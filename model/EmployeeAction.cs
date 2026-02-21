using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class EmployeeAction
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
    }
}
