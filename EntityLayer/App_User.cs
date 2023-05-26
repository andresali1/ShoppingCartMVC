using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class App_User
    {
        public int UserId { get; set; }
        public string U_name { get; set; }
        public string U_surname { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public bool IsReset { get; set; }
        public bool Active { get; set; }
    }
}