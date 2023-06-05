using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BL_Category
    {
        private DL_Category oDataLayer = new DL_Category();

        /// <summary>
        /// Method to get all categories in db
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            return oDataLayer.GetCategories();
        }

        /// <summary>
        /// Method to Add a new Category from admin page
        /// </summary>
        /// <param name="obj">Category object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public int AddCategory(Category obj, out string Message)
        {
            Message = string.Empty;

            if (string.IsNullOrEmpty(obj.C_description) || string.IsNullOrWhiteSpace(obj.C_description))
            {
                Message = "La descripción de la categoría no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Message))
            {
                return oDataLayer.AddCategory(obj, out Message);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Method to edit a Category from admin page
        /// </summary>
        /// <param name="obj">Category object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool EditCategory(Category obj, out string Message)
        {
            Message = string.Empty;

            if (string.IsNullOrEmpty(obj.C_description) || string.IsNullOrWhiteSpace(obj.C_description))
            {
                Message = "La descripción de la categoría no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Message))
            {
                return oDataLayer.EditCategory(obj, out Message);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method to delete a Category from admin page
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool DeleteCategory(int categoryId, out string Message)
        {
            return oDataLayer.DeleteCategory(categoryId, out Message);
        }
    }
}
