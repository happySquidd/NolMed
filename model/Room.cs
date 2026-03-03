using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Column("patient_id")]
        public int? PatientId { get; set; }
        [Column("room_number")]
        public int RoomNumber { get; set; }
        [Column("room_name")]
        public string? RoomName { get; set; }
    }
}
