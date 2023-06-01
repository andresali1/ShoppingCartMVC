using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DL_User
    {
        public List<App_User> GetUsers()
        {
            List<App_User> list = new List<App_User>();

            try
            {
                //using (SqlConnection oConection = new SqlConnection(Conection.connStr.Replace(@"\\", @"\")))
                using (SqlConnection oConection = new SqlConnection(Conection.connStr))
                {
                    string query = "SELECT UserId, U_name, U_surname, Email, Active FROM APP_USER";

                    SqlCommand cmd = new SqlCommand(query, oConection);
                    cmd.CommandType = CommandType.Text;

                    oConection.Open();

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
                }
            }
            catch (Exception e)
            {
                list = new List<App_User>();
            }

            return list;
        }
    }
}
