using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.model
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public object ViewTab { get; set; }
        public string RoleRequired { get; set; }
    }
}
