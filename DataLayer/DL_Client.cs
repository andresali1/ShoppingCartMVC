using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class DL_Client
    {
        /// <summary>
        /// Method to register a new Client
        /// </summary>
        /// <param name="obj">Client type object with the data</param>
        /// <param name="Message">Out message with information</param>
        /// <returns></returns>
        public int RegisterClient(Client obj, out string Message)
        {
            int generatedId = 0;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("SP_INS_CLIENT", oConnection);
                    cmd.Parameters.AddWithValue("C_name", obj.C_name);
                    cmd.Parameters.AddWithValue("C_surname", obj.C_surname);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Pass", obj.Pass);
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
        /// Method to get the client list
        /// </summary>
        /// <returns></returns>
        public List<Client> GetClients()
        {
            List<Client> list = new List<Client>();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    string query = "SELECT ClientId, C_name, C_surname, Email, Pass, IsReset FROM CLIENT";

                    SqlCommand cmd = new SqlCommand(query, oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Client()
                            {
                                ClientId = Convert.ToInt32(dr["ClientId"])
                                ,C_name = dr["C_name"].ToString()
                                ,C_surname = dr["C_surname"].ToString()
                                ,Email = dr["Email"].ToString()
                                ,Pass = dr["Pass"].ToString()
                                ,IsReset = Convert.ToBoolean(dr["IsReset"])
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Client>();
            }

            return list;
        }

        /// <summary>
        /// Method to change Client's password
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <param name="newPass">Given new password</param>
        /// <param name="Message">Out param with message</param>
        /// <returns></returns>
        public bool ChangePassword(int clientId, string newPass, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE CLIENT SET Pass = @newPass, IsReset = 0 WHERE ClientId = @ClientId", oConnection);
                    cmd.Parameters.AddWithValue("@newPass", newPass);
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
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

        /// <summary>
        /// Method to reset a client's forgotten password
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <param name="pass">Random generated Password</param>
        /// <param name="Message">Out param with message</param>
        /// <returns></returns>
        public bool ResetPassword(int clientId, string pass, out string Message)
        {
            bool result = false;
            Message = string.Empty;

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE CLIENT SET Pass = @pass, IsReset = 1 WHERE ClientId = @ClientId", oConnection);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
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
