using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Sale_Details
    {
        public int Sale_detailsId { get; set; }
        public int SaleId { get; set; }
        public Product oProduct { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
        public string TransactionId { get; set; }
    }
}