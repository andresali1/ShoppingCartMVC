using EntityLayer;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace DataLayer
{
    public class DL_Location
    {
        /// <summary>
        /// Method to gel all the departments
        /// </summary>
        /// <returns></returns>
        public List<Department> GetDepartmentList()
        {
            List<Department> list = new List<Department>();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    string query = "select * from DEPARTMENT";

                    SqlCommand cmd = new SqlCommand(query, oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Department()
                            {
                                DepartmentId = Convert.ToInt32(dr["DepartmentId"])
                                ,D_description = dr["D_description"].ToString()
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Department>();
            }

            return list;
        }

        /// <summary>
        /// Method to get all the towns by Department Id
        /// </summary>
        /// <param name="departmentId">Id of the department</param>
        /// <returns></returns>
        public List<Town> GetTownListByDepartmentId(int departmentId)
        {
            List<Town> list = new List<Town>();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    string query = "SELECT TownId, T_description FROM TOWN WHERE DepartmentId = @DepartmentId";

                    SqlCommand cmd = new SqlCommand(query, oConnection);
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Town()
                            {
                                TownId = Convert.ToInt32(dr["TownId"])
                                ,T_description = dr["T_description"].ToString()
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Town>();
            }

            return list;
        }

        /// <summary>
        /// Method to get all the Town location by Town Id
        /// </summary>
        /// <param name="townId">Id of the Town</param>
        /// <returns></returns>
        public List<Town_Location> GetTownLocationListByTownId(int townId)
        {
            List<Town_Location> list = new List<Town_Location>();

            try
            {
                using (SqlConnection oConnection = new SqlConnection(Conection.connStr))
                {
                    string query = "SELECT LocationId, L_description FROM TOWN_LOCATION WHERE TownId = @TownId";

                    SqlCommand cmd = new SqlCommand(query, oConnection);
                    cmd.Parameters.AddWithValue("@TownId", townId);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Town_Location()
                            {
                                LocationId = dr["LocationId"].ToString()
                                ,L_description = dr["L_description"].ToString()
                            });
                        }
                    }

                    oConnection.Close();
                }
            }
            catch (Exception e)
            {
                list = new List<Town_Location>();
            }

            return list;
        }
    }
}
