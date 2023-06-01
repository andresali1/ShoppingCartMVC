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

        /// <summary>
        /// Method to Add a new user from admin page
        /// </summary>
        /// <param name="obj">App_User object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public int AddUser(App_User obj, out string Message)
        {
            Message = string.Empty;

            if (string.IsNullOrEmpty(obj.U_name) || string.IsNullOrWhiteSpace(obj.U_name))
            {
                Message = "El nombre del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.U_surname) || string.IsNullOrWhiteSpace(obj.U_surname))
            {
                Message = "El apellido del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Message = "El correo del usuario no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Message))
            {
                string clave = "test123";

                return oDataLayer.AddUser(obj, out Message);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Method to edit an user from admin page
        /// </summary>
        /// <param name="obj">App_User object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool EditUser(App_User obj, out string Message)
        {
            return oDataLayer.EditUser(obj, out Message);
        }
    }
}
