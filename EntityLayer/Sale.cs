using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int ClientId { get; set; }
        public int ProductAmount { get; set; }
        public decimal Total { get; set; }
        public string Contact { get; set; }
        public string LocationId { get; set; }
        public string PhoneNumber { get; set; }
        public string S_address { get; set; }
        public string TransactionId { get; set; }
        public List<Sale_Details> oSale_Details { get; set; }
    }
}
