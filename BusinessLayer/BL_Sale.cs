using DataLayer;
using EntityLayer;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer
{
    public class BL_Sale
    {
        private DL_Sale oDataLayer = new DL_Sale();

        /// <summary>
        /// Method to register a Sale in DB
        /// </summary>
        /// <param name="obj">Sale type object</param>
        /// <param name="saleDetail">Datatable with Shopping Cart Data</param>
        /// <param name="Message">Out param with message</param>
        /// <returns></returns>
        public bool RegisterSale(Sale obj, DataTable saleDetail, out string Message)
        {
            return oDataLayer.RegisterSale(obj, saleDetail, out Message);
        }

        /// <summary>
        /// Method to get the sale record of a client
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <returns></returns>
        public List<Sale_Details> GetSaleRecord(int clientId)
        {
            return oDataLayer.GetSaleRecord(clientId);
        }
    }
}
