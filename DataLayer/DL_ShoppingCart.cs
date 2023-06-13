using System;
using System.Data.SqlClient;
using System.Data;
using EntityLayer;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DataLayer
{
    public class DL_ShoppingCart
    {
        /// <summary>
        /// Method to know if there is at least one product in a Client's shopping cart
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <param name="productId">Id of the Product</param>
        /// <returns></returns>
        public bool CartExists(int clientId, int productId)
        {
            bool result = true;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_SHOPPINGCART_EXISTS", oConnection);
                    cmd.Parameters.AddWithValue("ClientId", clientId);
                    cmd.Parameters.AddWithValue("ProductId", productId);
                    cmd.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConnection.Open();

                    cmd.ExecuteNonQuery();

                    result = Convert.ToBoolean(cmd.Parameters["Result"].Value);

                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Method to add products to a Client's shopping cart
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <param name="productId">Id of the Product</param>
        /// <param name="add">Is adding (true - false)</param>
        /// <param name="Message">Out param with message</param>
        /// <returns></returns>
        public bool ManageProductCartAmount(int clientId, int productId, bool add, out string Message)
        {
            bool result = true;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_INS_SHOPPINGCART", oConnection);
                    cmd.Parameters.AddWithValue("ClientId", clientId);
                    cmd.Parameters.AddWithValue("ProductId", productId);
                    cmd.Parameters.AddWithValue("Add", add);
                    cmd.Parameters.Add("Message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConnection.Open();

                    cmd.ExecuteNonQuery();

                    result = Convert.ToBoolean(cmd.Parameters["Result"].Value);
                    Message = cmd.Parameters["Message"].Value.ToString();

                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
                Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Method to get the amount of products of one client's propduct cart
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <returns></returns>
        public int GetShoppingCartAmount(int clientId)
        {
            int result = 0;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM SHOPPING_CART WHERE ClientId = @ClientId", oConnection);
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    result = Convert.ToInt32(cmd.ExecuteScalar());

                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Method to get all the products in the shopping cart of a CLient
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <returns></returns>
        public List<Shopping_Cart> GetProductsInCart(int clientId)
        {
            List<Shopping_Cart> list = new List<Shopping_Cart>();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    string query = "SELECT * FROM FN_GETSHOPPINGCART_CLIENT(@ClientId)";

                    SqlCommand cmd = new SqlCommand(query, oConnection);
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Shopping_Cart()
                            {
                                oProduct = new Product()
                                {
                                    ProductId = Convert.ToInt32(dr["ProductId"])
                                    ,P_name = dr["P_name"].ToString()
                                    ,Price = Convert.ToDecimal(dr["Price"], new CultureInfo("es-CO"))
                                    ,ImageRoute = dr["ImageRoute"].ToString()
                                    ,ImageName = dr["ImageName"].ToString()
                                    ,oBrand = new Brand() { B_description = dr["Brand"].ToString() }
                                },
                                Amount = Convert.ToInt32(dr["Amount"])
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Shopping_Cart>();
            }

            return list;
        }

        /// <summary>
        /// Method to delete a product from client's shopping cart
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <param name="productId">Id of the Product</param>
        /// <returns></returns>
        public bool DeleteCartProduct(int clientId, int productId)
        {
            bool result = true;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_DEL_SHOPPINGCART_PRODUCT", oConnection);
                    cmd.Parameters.AddWithValue("ClientId", clientId);
                    cmd.Parameters.AddWithValue("ProductId", productId);
                    cmd.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConnection.Open();

                    cmd.ExecuteNonQuery();

                    result = Convert.ToBoolean(cmd.Parameters["Result"].Value);

                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}
