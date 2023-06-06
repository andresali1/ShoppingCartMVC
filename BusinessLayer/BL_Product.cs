using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BL_Product
    {
        private DL_Product oDataLayer = new DL_Product();

        /// <summary>
        /// Method to get all products in db
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            return oDataLayer.GetProducts();
        }

        /// <summary>
        /// Method to Add a new Product from admin page
        /// </summary>
        /// <param name="obj">Product object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public int AddProduct(Product obj, out string Message)
        {
            Message = string.Empty;

            if (string.IsNullOrEmpty(obj.P_name) || string.IsNullOrWhiteSpace(obj.P_name))
            {
                Message = "El nombre del producto no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.P_description) || string.IsNullOrWhiteSpace(obj.P_description))
            {
                Message = "La descripción del producto no puede ser vacío";
            }
            else if(obj.oBrand.BrandId == 0)
            {
                Message = "Debe seleccionar una marca";
            }
            else if(obj.oCategory.CategoryId == 0)
            {
                Message = "Debe seleccionar una categoría";
            }
            else if(obj.Price == 0)
            {
                Message = "Debe ingresar el precio del producto";
            }
            else if (obj.Stock == 0)
            {
                Message = "Debe ingresar el stock del producto";
            }

            if (string.IsNullOrEmpty(Message))
            {
                return oDataLayer.AddProduct(obj, out Message);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Method to edit a Product from admin page
        /// </summary>
        /// <param name="obj">Product object type with the data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool EditProduct(Product obj, out string Message)
        {
            Message = string.Empty;

            if (string.IsNullOrEmpty(obj.P_name) || string.IsNullOrWhiteSpace(obj.P_name))
            {
                Message = "El nombre del producto no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.P_description) || string.IsNullOrWhiteSpace(obj.P_description))
            {
                Message = "La descripción del producto no puede ser vacío";
            }
            else if (obj.oBrand.BrandId == 0)
            {
                Message = "Debe seleccionar una marca";
            }
            else if (obj.oCategory.CategoryId == 0)
            {
                Message = "Debe seleccionar una categoría";
            }
            else if (obj.Price == 0)
            {
                Message = "Debe ingresar el precio del producto";
            }
            else if (obj.Stock == 0)
            {
                Message = "Debe ingresar el stock del producto";
            }

            if (string.IsNullOrEmpty(Message))
            {
                return oDataLayer.EditProduct(obj, out Message);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method to update image data of a Product in DB
        /// </summary>
        /// <param name="obj">Product type object with data</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool SaveImageData(Product obj, out string Message)
        {
            return oDataLayer.SaveImageData(obj, out Message);
        }

        /// <summary>
        /// Method to delete a Product from admin page
        /// </summary>
        /// <param name="ProductId">Product Id</param>
        /// <param name="Message">output param with message</param>
        /// <returns></returns>
        public bool DeleteProduct(int ProductId, out string Message)
        {
            return oDataLayer.DeleteProduct(ProductId, out Message);
        }
    }
}
