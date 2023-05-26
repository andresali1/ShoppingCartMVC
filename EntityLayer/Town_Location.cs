using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Town_Location
    {
        public string LocationId { get; set; }
        public string L_description { get; set; }
        public Town oTown { get; set; }
    }
}
