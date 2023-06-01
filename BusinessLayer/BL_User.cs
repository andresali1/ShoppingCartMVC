using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BL_User
    {
        private DL_User oDataLayer = new DL_User();

        /// <summary>
        /// Method to get all users in db
        /// </summary>
        /// <returns></returns>
        public List<App_User> GetUsers()
        {
            return oDataLayer.GetUsers();
        }
    }
}
