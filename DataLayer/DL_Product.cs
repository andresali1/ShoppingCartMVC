using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Globalization;

namespace DataLayer
{
    public class DL_Product
    {
        /// <summary>
        /// Method to get all products in db
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            List<Product> list = new List<Product>();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT P.ProductId, P.P_name, P.P_description, B.BrandId, B.B_description,");
                    sb.AppendLine("C.CategoryId, C.C_description, P.Price, P.Stock,");
                    sb.AppendLine("P.ImageRoute, P.ImageName, P.Active ");
                    sb.AppendLine("FROM PRODUCT P ");
                    sb.AppendLine("INNER JOIN BRAND B ON B.BrandId = P.BrandId ");
                    sb.AppendLine("INNER JOIN CATEGORY C ON C.CategoryId = P.CategoryId");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Product()
                            {
                                ProductId = Convert.ToInt32(dr["ProductId"])
                                ,P_name = dr["P_name"].ToString()
                                ,P_description = dr["P_description"].ToString()
                                ,oBrand = new Brand() { BrandId = Convert.ToInt32(dr["BrandId"]), B_description = dr["B_description"].ToString() }
                                ,oCategory = new Category() { CategoryId = Convert.ToInt32(dr["CategoryId"]), C_description = dr["C_description"].ToString() }
                                ,Price = Convert.ToDecimal(dr["Price"], new CultureInfo("es-CO"))
                                ,Stock = Convert.ToInt32(dr["Stock"])
                                ,ImageRoute = dr["ImageRoute"].ToString()
                                ,ImageName = dr["ImageName"].ToString()
                                ,Active = Convert.ToBoolean(dr["Active"])
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Product>();
            }

            return list;
        }

        /// <summary>
        /// Method to Add a new Product from admin page
        /// </summary>
        /// <param name="obj">Product object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public int AddProduct(Product obj, out string Message)
        {
            int generatedId = 0;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_INS_PRODUCT", oConnection);
                    cmd.Parameters.AddWithValue("P_name", obj.P_name);
                    cmd.Parameters.AddWithValue("P_description", obj.P_description);
                    cmd.Parameters.AddWithValue("BrandId", obj.oBrand.BrandId);
                    cmd.Parameters.AddWithValue("CategoryId", obj.oCategory.CategoryId);
                    cmd.Parameters.AddWithValue("Price", obj.Price);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Active", obj.Active);
                    cmd.Parameters.Add("Message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConnection.Open();

                    cmd.ExecuteNonQuery();

                    generatedId = Convert.ToInt32(cmd.Parameters["Result"].Value);
                    Message = cmd.Parameters["Message"].Value.ToString();

                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                generatedId = 0;
                Message = ex.Message;
            }

            return generatedId;
        }

        /// <summary>
        /// Method to edit a Product from admin page
        /// </summary>
        /// <param name="obj">Product object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool EditProduct(Product obj, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_UPD_PRODUCT", oConnection);
                    cmd.Parameters.AddWithValue("ProductId", obj.ProductId);
                    cmd.Parameters.AddWithValue("P_name", obj.P_name);
                    cmd.Parameters.AddWithValue("P_description", obj.P_description);
                    cmd.Parameters.AddWithValue("BrandId", obj.oBrand.BrandId);
                    cmd.Parameters.AddWithValue("CategoryId", obj.oCategory.CategoryId);
                    cmd.Parameters.AddWithValue("Price", obj.Price);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Active", obj.Active);
                    cmd.Parameters.Add("Message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Result", SqlDbType.Int).Direction = ParameterDirection.Output;
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
        /// Method to update image data of a Product in DB
        /// </summary>
        /// <param name="obj">Product type object with data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool SaveImageData(Product obj, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    string query = "UPDATE PRODUCT SET ImageRoute = @ImageRoute, ImageName = @ImageName WHERE ProductId = @ProductId";

                    SqlCommand cmd = new SqlCommand(query, oConnection);

                    cmd.Parameters.AddWithValue("@ImageRoute", obj.ImageRoute);
                    cmd.Parameters.AddWithValue("@ImageName", obj.ImageName);
                    cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        Message = "No se pudo actualizar imágen";
                    }

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
        /// Method to delete a Product from admin page
        /// </summary>
        /// <param name="brandId">Product Id</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool DeleteProduct(int productId, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_DEL_PRODUCT", oConnection);
                    cmd.Parameters.AddWithValue("ProductId", productId);
                    cmd.Parameters.Add("Message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Result", SqlDbType.Int).Direction = ParameterDirection.Output;
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
    }
}
