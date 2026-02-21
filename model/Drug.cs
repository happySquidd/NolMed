using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class Drug
    {
        [Key]
        public int Id { get; set; }
        [Column("drug_name")]
        public string DrugName { get; set; }
        public int Count { get; set; }
    }
}
