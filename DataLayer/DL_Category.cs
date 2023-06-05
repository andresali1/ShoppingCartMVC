using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class DL_Category
    {
        /// <summary>
        /// Method to get all categories in db
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();

            try
            {
                //using (SqlConnection oConection = new SqlConnection(Conection.connStr.Replace(@"\\", @"\")))
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    string query = "SELECT CategoryId, C_description, Active FROM CATEGORY";

                    SqlCommand cmd = new SqlCommand(query, oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Category()
                            {
                                CategoryId = Convert.ToInt32(dr["CategoryId"])
                                ,C_description = dr["C_description"].ToString()
                                ,Active = Convert.ToBoolean(dr["Active"])
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Category>();
            }

            return list;
        }

        /// <summary>
        /// Method to Add a new category from admin page
        /// </summary>
        /// <param name="obj">Category object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public int AddCategory(Category obj, out string Message)
        {
            int generatedId = 0;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_INS_CATEGORY", oConnection);
                    cmd.Parameters.AddWithValue("C_description", obj.C_description);
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
        /// Method to edit a Category from admin page
        /// </summary>
        /// <param name="obj">Category object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool EditCategory(Category obj, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_UPD_CATEGORY", oConnection);
                    cmd.Parameters.AddWithValue("CategoryId", obj.CategoryId);
                    cmd.Parameters.AddWithValue("C_description", obj.C_description);
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
        /// Method to delete a Category from admin page
        /// </summary>
        /// <param name="userId">Category Id</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool DeleteCategory(int categoryId, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_DEL_CATEGORY", oConnection);
                    cmd.Parameters.AddWithValue("CategoryId", categoryId);
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
