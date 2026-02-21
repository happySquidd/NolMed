using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class Vitals
    {
        [Key]
        public int Id { get; set; }
        [Column("visit_id")]
        public int VisitId { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public decimal Temperature { get; set; }
        public int Bpm { get; set; }
        public DateTime Time { get; set; }
    }
}
