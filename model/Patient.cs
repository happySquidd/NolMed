using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        public int Mrn { get; set; }
        public DateOnly Dob { get; set; }
        public string Blood { get; set; }
        public bool InPatient { get; set; }
    }
}
