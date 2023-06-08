using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BL_Client
    {
        private DL_Client oDataLayer = new DL_Client();

        /// <summary>
        /// Method to register a new Client
        /// </summary>
        /// <param name="obj">Client type object with the data</param>
        /// <param name="Message">Out message with information</param>
        /// <returns></returns>
        public int RegisterClient(Client obj, out string Message)
        {
            Message = string.Empty;

            if (string.IsNullOrEmpty(obj.C_name) || string.IsNullOrWhiteSpace(obj.C_name))
            {
                Message = "El nombre del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.C_surname) || string.IsNullOrWhiteSpace(obj.C_surname))
            {
                Message = "El apellido del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Message = "El correo del usuario no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Message))
            {
                obj.Pass = BL_Resources.Sha256Converter(obj.Pass);
                return oDataLayer.RegisterClient(obj, out Message);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Method to get the client list
        /// </summary>
        /// <returns></returns>
        public List<Client> GetClients()
        {
            return oDataLayer.GetClients();
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
            return oDataLayer.ChangePassword(clientId, newPass, out Message);
        }

        /// <summary>
        /// Method to reset a client's forgotten password
        /// </summary>
        /// <param name="clientId">Id of the Client</param>
        /// <param name="clientEmail">Email of the client</param>
        /// <param name="Message">Out param with message</param>
        /// <returns></returns>
        public bool ResetPassword(int clientId, string clientEmail, out string Message)
        {
            Message = string.Empty;
            string newPass = BL_Resources.GeneratePass();
            bool result = oDataLayer.ResetPassword(clientId, BL_Resources.Sha256Converter(newPass), out Message);

            if (result)
            {
                string subject = "Contraseña Reestablecida CarritoC";
                string email_message = "<h3>Su cuenta fue reestablecida correctamente</h3></br><p>Su contraseña para acceder ahora es: !pass!</p>";
                email_message = email_message.Replace("!pass!", newPass);

                bool response = BL_Resources.SendEmail(clientEmail, subject, email_message);

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
