using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class EmployeeAction
    {
        [Key]
        public int Id { get; set; }
        [Column("employee_id")]
        public int EmployeeId { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
    }
}
