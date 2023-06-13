using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BL_Location
    {
        private DL_Location oDataLayer = new DL_Location();

        /// <summary>
        /// Method to gel all the departments
        /// </summary>
        /// <returns></returns>
        public List<Department> GetDepartmentList()
        {
            return oDataLayer.GetDepartmentList();
        }

        /// <summary>
        /// Method to get all the towns by Department Id
        /// </summary>
        /// <param name="departmentId">Id of the department</param>
        /// <returns></returns>
        public List<Town> GetTownListByDepartmentId(int departmentId)
        {
            return oDataLayer.GetTownListByDepartmentId(departmentId);
        }

        /// <summary>
        /// Method to get all the Town location by Town Id
        /// </summary>
        /// <param name="townId">Id of the Town</param>
        /// <returns></returns>
        public List<Town_Location> GetTownLocationListByTownId(int townId)
        {
            return oDataLayer.GetTownLocationListByTownId(townId);
        }
    }
}
