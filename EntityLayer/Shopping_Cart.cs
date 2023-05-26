using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Shopping_Cart
    {
        public int CartId { get; set; }
        public Client oClient { get; set; }
        public Product oProduct { get; set; }
        public int AMOUNT { get; set; }
    }
}
