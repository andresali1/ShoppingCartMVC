using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Client
    {
        public int ClientId { get; set; }
        public string C_name { get; set; }
        public string C_surname { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public bool IsReset { get; set; }
    }
}
