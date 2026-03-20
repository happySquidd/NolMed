using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class ErOverviewBox
    {
        public int RoomNumber { get; set; }
        public string? RoomName { get; set; }
        public string? PatientName { get; set; }
        public decimal? Temperature { get; set; }
        public int? HeartRate { get; set; }
        public string? BackgroundColor { get; set; }
    }
}
