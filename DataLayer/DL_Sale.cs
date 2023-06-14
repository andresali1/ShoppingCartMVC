using EntityLayer;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DataLayer
{
    public class DL_Sale
    {
        /// <summary>
        /// Method to register a Sale in DB
        /// </summary>
        /// <param name="obj">Sale type object</param>
        /// <param name="saleDetail">Datatable with Shopping Cart Data</param>
        /// <param name="Message">Out param with message</param>
        /// <returns></returns>
        public bool RegisterSale(Sale obj, DataTable saleDetail, out string Message)
        {
            bool response = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_INS_SALE", oConnection);
                    cmd.Parameters.AddWithValue("ClientId", obj.ClientId);
                    cmd.Parameters.AddWithValue("ProductAmount", obj.ProductAmount);
                    cmd.Parameters.AddWithValue("Total", obj.Total);
                    cmd.Parameters.AddWithValue("Contact", obj.Contact);
                    cmd.Parameters.AddWithValue("LocationId", obj.LocationId);
                    cmd.Parameters.AddWithValue("PhoneNumber", obj.PhoneNumber);
                    cmd.Parameters.AddWithValue("S_address", obj.S_address);
                    cmd.Parameters.AddWithValue("TransactionId", obj.TransactionId);
                    cmd.Parameters.AddWithValue("Sale_Details", saleDetail);
                    cmd.Parameters.Add("Message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConnection.Open();

                    cmd.ExecuteNonQuery();

                    response = Convert.ToBoolean(cmd.Parameters["Result"].Value);
                    Message = cmd.Parameters["Message"].Value.ToString();

                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                response = false;
                Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Method to get the sale record of a client
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <returns></returns>
        public List<Sale_Details> GetSaleRecord(int clientId)
        {
            List<Sale_Details> list = new List<Sale_Details>();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    string query = "SELECT * FROM FN_CON_SALE(@ClientId)";

                    SqlCommand cmd = new SqlCommand(query, oConnection);
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Sale_Details()
                            {
                                oProduct = new Product()
                                {
                                    P_name = dr["P_name"].ToString()
                                    ,Price = Convert.ToDecimal(dr["Price"], new CultureInfo("es-CO"))
                                    ,ImageRoute = dr["ImageRoute"].ToString()
                                    ,ImageName = dr["ImageName"].ToString()
                                }
                                ,Amount = Convert.ToInt32(dr["Amount"])
                                ,Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-CO"))
                                ,TransactionId = dr["TransactionId"].ToString()
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Sale_Details>();
            }

            return list;
        }
    }
}
