using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BL_Brand
    {
        private DL_Brand oDataLayer = new DL_Brand();

        /// <summary>
        /// Method to get all brands in db
        /// </summary>
        /// <returns></returns>
        public List<Brand> GetBrands()
        {
            return oDataLayer.GetBrands();
        }

        /// <summary>
        /// Method to Add a new Brand from admin page
        /// </summary>
        /// <param name="obj">Brand object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public int AddBrand(Brand obj, out string Message)
        {
            Message = string.Empty;

            if (string.IsNullOrEmpty(obj.B_description) || string.IsNullOrWhiteSpace(obj.B_description))
            {
                Message = "La descripción de la marca no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Message))
            {
                return oDataLayer.AddBrand(obj, out Message);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Method to edit a Brand from admin page
        /// </summary>
        /// <param name="obj">Brand object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool EditBrand(Brand obj, out string Message)
        {
            Message = string.Empty;

            if (string.IsNullOrEmpty(obj.B_description) || string.IsNullOrWhiteSpace(obj.B_description))
            {
                Message = "La descripción de la marca no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Message))
            {
                return oDataLayer.EditBrand(obj, out Message);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method to delete a Brand from admin page
        /// </summary>
        /// <param name="brandId">Brand Id</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool DeleteBrand(int brandId, out string Message)
        {
            return oDataLayer.DeleteBrand(brandId, out Message);
        }
    }
}
