using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BL_User
    {
        private DL_User oDataLayer = new DL_User();

        public List<App_User> GetUsers()
        {
            return oDataLayer.GetUsers();
        }
    }
}
