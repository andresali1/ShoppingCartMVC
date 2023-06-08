using DataLayer;
using EntityLayer;
using System.Collections.Generic;


namespace BusinessLayer
{
    public class BL_Report
    {
        private DL_Report oDataLayer = new DL_Report();

        /// <summary>
        /// Method to get the report for the dashboard
        /// </summary>
        /// <returns></returns>
        public Dashboard GetDashboardReport()
        {
            return oDataLayer.GetDashboardReport();
        }

        /// <summary>
        /// Method to get a sales report of given data
        /// </summary>
        /// <param name="beginDate">Min date to search to</param>
        /// <param name="endDate">Max date to search to</param>
        /// <param name="transactionId">Id of the transaction</param>
        /// <returns></returns>
        public List<Report> GetSalesReport(string beginDate, string endDate, string transactionId)
        {
            return oDataLayer.GetSalesReport(beginDate, endDate, transactionId);
        }
    }
}
