using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DL_User
    {
        /// <summary>
        /// Method to get all users in db
        /// </summary>
        /// <returns></returns>
        public List<App_User> GetUsers()
        {
            List<App_User> list = new List<App_User>();

            try
            {
                //using (SqlConnection oConection = new SqlConnection(Conection.connStr.Replace(@"\\", @"\")))
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    string query = "SELECT UserId, U_name, U_surname, Email, Active FROM APP_USER";

                    SqlCommand cmd = new SqlCommand(query, oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new App_User()
                            {
                                UserId = Convert.ToInt32(dr["UserId"])
                                ,U_name = dr["U_name"].ToString()
                                ,U_surname = dr["U_surname"].ToString()
                                ,Email = dr["Email"].ToString()
                                ,Active = Convert.ToBoolean(dr["Active"])
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<App_User>();
            }

            return list;
        }

        /// <summary>
        /// Method to Add a new user from admin page
        /// </summary>
        /// <param name="obj">App_User object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public int AddUser(App_User obj, out string Message)
        {
            int generatedId = 0;
            Message = string.Empty;

            try
            {
                using(SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_INS_APP_USER", oConnection);
                    cmd.Parameters.AddWithValue("U_name", obj.U_name);
                    cmd.Parameters.AddWithValue("U_surname", obj.U_surname);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Pass", obj.Pass);
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
        /// Method to edit an user from admin page
        /// </summary>
        /// <param name="obj">App_User object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool EditUser(App_User obj, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_UPD_APP_USER", oConnection);
                    cmd.Parameters.AddWithValue("UserId", obj.UserId);
                    cmd.Parameters.AddWithValue("U_name", obj.U_name);
                    cmd.Parameters.AddWithValue("U_surname", obj.U_surname);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
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
        /// Method to delete an User from admin page
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool DeleteUser(int userId, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("DELETE TOP(1) FROM APP_USER WHERE UserId = @UserId", oConnection);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    result = cmd.ExecuteNonQuery() > 0 ? true : false;

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
