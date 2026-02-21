using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class Insurance
    {
        [Key]
        public int Id { get; set; }
        [Column("patient_id")]
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
