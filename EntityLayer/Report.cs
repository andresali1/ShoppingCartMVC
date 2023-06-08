using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Report
    {
        public string SaleDate { get; set; }
        public string R_client { get; set; }
        public string R_product { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
        public string TransactionId { get; set; }
    }
}
