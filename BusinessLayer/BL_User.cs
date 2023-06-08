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
                string pass = BL_Resources.GeneratePass();

                string subject = "Creación de Cuenta CarritoC";
                string email_message = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !pass!</p>";
                email_message = email_message.Replace("!pass!", pass);

                bool response = BL_Resources.SendEmail(obj.Email, subject, email_message);

                if (response)
                {
                    obj.Pass = BL_Resources.Sha256Converter(pass);
                    return oDataLayer.AddUser(obj, out Message);
                }
                else
                {
                    Message = "No se pudo enviar el correo";
                    return 0;
                }
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
                return oDataLayer.EditUser(obj, out Message);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method to delete an User from admin page
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool DeleteUser(int userId, out string Message)
        {
            return oDataLayer.DeleteUser(userId, out Message);
        }

        /// <summary>
        /// Method to change user's password
        /// </summary>
        /// <param name="userId">User's Id</param>
        /// <param name="newPass">Given new pass</param>
        /// <param name="Message">Out param with message</param>
        /// <returns></returns>
        public bool ChangePassword(int userId, string newPass, out string Message)
        {
            return oDataLayer.ChangePassword(userId, newPass, out Message);
        }

        /// <summary>
        /// Method to reset the user's password
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="pass">New random pass</param>
        /// <param name="Message">Out param with message</param>
        /// <returns></returns>
        public bool ResetPassword(int userId, string userEmail, out string Message)
        {
            Message = string.Empty;
            string newPass = BL_Resources.GeneratePass();
            bool result = oDataLayer.ResetPassword(userId, BL_Resources.Sha256Converter(newPass), out Message);

            if (result)
            {
                string subject = "Contraseña Reestablecida CarritoC";
                string email_message = "<h3>Su cuenta fue reestablecida correctamente</h3></br><p>Su contraseña para acceder ahora es: !pass!</p>";
                email_message = email_message.Replace("!pass!", newPass);

                bool response = BL_Resources.SendEmail(userEmail, subject, email_message);

                if (response)
                {
                    return true;
                }
                else
                {
                    Message = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Message = "No se pudo reestablecer la contraseña";
                return false;
            }
        }
    }
}
