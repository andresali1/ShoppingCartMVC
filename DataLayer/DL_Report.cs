using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DL_Report
    {
        /// <summary>
        /// Method to get the report for the dashboard
        /// </summary>
        /// <returns></returns>
        public Dashboard GetDashboardReport()
        {
            Dashboard obj = new Dashboard();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_CON_DashboardReport", oConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Dashboard()
                            {
                                ClientTotal = Convert.ToInt32(dr["ClientTotal"]),
                                SaleTotal = Convert.ToInt32(dr["SaleTotal"]),
                                ProductTotal = Convert.ToInt32(dr["ProductTotal"])
                            };
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                obj = new Dashboard();
            }

            return obj;
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
            List<Report> list = new List<Report>();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_CON_SalesReport", oConnection);
                    cmd.Parameters.AddWithValue("BeginDate", beginDate);
                    cmd.Parameters.AddWithValue("EndDate", endDate);
                    cmd.Parameters.AddWithValue("TransactionId", transactionId);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Report()
                            {
                                SaleDate = dr["SaleDate"].ToString()
                                ,R_client = dr["R_client"].ToString()
                                ,R_product = dr["R_product"].ToString()
                                ,Price = Convert.ToDecimal(dr["Price"])
                                ,Amount = Convert.ToInt32(dr["Amount"])
                                ,Total = Convert.ToDecimal(dr["Total"])
                                ,TransactionId = dr["TransactionId"].ToString()
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Report>();
            }

            return list;
        }
    }
}
