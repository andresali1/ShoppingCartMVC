using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Town
    {
        public int TownId { get; set; }
        public string T_description { get; set; }
        public Department oDepartment { get; set; }
    }
}
