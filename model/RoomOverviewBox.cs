using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class RoomOverviewBox
    {
        public int RoomNumber { get; set; }
        public int RoomId { get; set; }
        public string? PatientName { get; set; }
        public string? RoomName { get; set; }
        public string BackgroundColor { get; set; }
    }
}
