using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace DataLayer
{
    public class DL_Brand
    {
        /// <summary>
        /// Method to get all brands in db
        /// </summary>
        /// <returns></returns>
        public List<Brand> GetBrands()
        {
            List<Brand> list = new List<Brand>();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    string query = "SELECT BrandId, B_description, Active FROM BRAND";

                    SqlCommand cmd = new SqlCommand(query, oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Brand()
                            {
                                BrandId = Convert.ToInt32(dr["BrandId"])
                                ,B_description = dr["B_description"].ToString()
                                ,Active = Convert.ToBoolean(dr["Active"])
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Brand>();
            }

            return list;
        }

        /// <summary>
        /// Method to Add a new Brand from admin page
        /// </summary>
        /// <param name="obj">Brand object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public int AddBrand(Brand obj, out string Message)
        {
            int generatedId = 0;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_INS_BRAND", oConnection);
                    cmd.Parameters.AddWithValue("B_description", obj.B_description);
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
        /// Method to edit a Brand from admin page
        /// </summary>
        /// <param name="obj">Brand object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool EditBrand(Brand obj, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_UPD_BRAND", oConnection);
                    cmd.Parameters.AddWithValue("BrandId", obj.BrandId);
                    cmd.Parameters.AddWithValue("B_description", obj.B_description);
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
        /// Method to delete a Brand from admin page
        /// </summary>
        /// <param name="brandId">Brand Id</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool DeleteBrand(int brandId, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_DEL_BRAND", oConnection);
                    cmd.Parameters.AddWithValue("BrandId", brandId);
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
        /// Method to get all brands by id of the category
        /// </summary>
        /// <param name="categoryId">Id of the category</param>
        /// <returns></returns>
        public List<Brand> GetBrandsByCategory(int categoryId)
        {
            List<Brand> list = new List<Brand>();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT DISTINCT B.BrandId, B.B_description FROM PRODUCT P");
                    sb.AppendLine("INNER JOIN CATEGORY C ON C.CategoryId = P.CategoryId");
                    sb.AppendLine("INNER JOIN BRAND B ON B.BrandId = P.BrandId AND B.Active = 1");
                    sb.AppendLine("WHERE C.CategoryId = IIF(@categoryId = 0, C.CategoryId, @categoryId)");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConnection);
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Brand()
                            {
                                BrandId = Convert.ToInt32(dr["BrandId"])
                                ,B_description = dr["B_description"].ToString()
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Brand>();
            }

            return list;
        }
    }
}
