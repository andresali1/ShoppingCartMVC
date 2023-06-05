using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

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
                                ,Price = Convert.ToDecimal(dr["Price"])
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
    }
}
