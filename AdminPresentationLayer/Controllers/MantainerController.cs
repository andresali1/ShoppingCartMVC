using BusinessLayer;
using EntityLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPresentationLayer.Controllers
{
    public class MantainerController : Controller
    {
        // GET: Mantainer
        public ActionResult Category()
        {
            return View();
        }

        // GET: Brand
        public ActionResult Brand()
        {
            return View();
        }

        // GET: Product
        public ActionResult Product()
        {
            return View();
        }

        /******************** CATEGORY ********************/
        #region Category
        /// <summary>
        /// Method to get all categories in db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCategories()
        {
            List<Category> oList = new List<Category>();
            oList = new BL_Category().GetCategories();

            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to save a Category in DB, Created or edited
        /// </summary>
        /// <param name="obj">Object with the category's data</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveCategory(Category obj)
        {
            object result;
            string message = string.Empty;

            if (obj.CategoryId == 0)
            {
                result = new BL_Category().AddCategory(obj, out message);
            }
            else
            {
                result = new BL_Category().EditCategory(obj, out message);
            }

            return Json(new { result, message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to delete a Category from Admin page
        /// </summary>
        /// <param name="categoryId">category's Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteCategory(int categoryId)
        {
            bool response = false;
            string message = string.Empty;

            response = new BL_Category().DeleteCategory(categoryId, out message);

            return Json(new { result = response, message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /******************** BRAND ********************/
        #region Brand
        /// <summary>
        /// Method to get all brands in db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetBrands()
        {
            List<Brand> oList = new List<Brand>();
            oList = new BL_Brand().GetBrands();

            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to save a Brand in DB, Created or edited
        /// </summary>
        /// <param name="obj">Object with the brand's data</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveBrand(Brand obj)
        {
            object result;
            string message = string.Empty;

            if (obj.BrandId == 0)
            {
                result = new BL_Brand().AddBrand(obj, out message);
            }
            else
            {
                result = new BL_Brand().EditBrand(obj, out message);
            }

            return Json(new { result, message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to delete a Brand from Admin page
        /// </summary>
        /// <param name="brandId">brand's Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteBrand(int brandId)
        {
            bool response = false;
            string message = string.Empty;

            response = new BL_Brand().DeleteBrand(brandId, out message);

            return Json(new { result = response, message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /******************** PRODUCT ********************/
        #region Product
        /// <summary>
        /// Method to get all products in db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetProducts()
        {
            List<Product> oList = new List<Product>();
            oList = new BL_Product().GetProducts();

            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to save a Product in DB, Created or editedm then saves their image if exists
        /// </summary>
        /// <param name="obj">Object with the product's data</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveProduct(string strObject, HttpPostedFileBase imageFile)
        {
            string message = string.Empty;
            bool successfulOp = true;
            bool img_success_save = true;

            Product obj = new Product();
            obj = JsonConvert.DeserializeObject<Product>(strObject);

            decimal price;

            if(decimal.TryParse(obj.PriceStr, NumberStyles.AllowDecimalPoint, new CultureInfo("es-CO"), out price))
            {
                obj.Price = price;
            }
            else
            {
                return Json(new { successfulOp = false, message = "El formato del precio debe ser #.##" }, JsonRequestBehavior.AllowGet);
            }

            if (obj.ProductId == 0)
            {
                int generatedProductId = new BL_Product().AddProduct(obj, out message);

                if(generatedProductId != 0)
                {
                    obj.ProductId = generatedProductId;
                }
                else
                {
                    successfulOp = false;
                }
            }
            else
            {
                successfulOp = new BL_Product().EditProduct(obj, out message);
            }

            if (successfulOp)
            {
                if(imageFile != null)
                {
                    string save_path = ConfigurationManager.AppSettings["PhotoServer"];
                    string extension = Path.GetExtension(imageFile.FileName);
                    string image_name = string.Concat(obj.ProductId.ToString(), extension);

                    try
                    {
                        imageFile.SaveAs(Path.Combine(save_path, image_name));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        img_success_save = false;
                    }

                    if (img_success_save)
                    {
                        obj.ImageRoute = save_path;
                        obj.ImageName = image_name;

                        bool res = new BL_Product().SaveImageData(obj, out message);
                    }
                    else
                    {
                        message = "Se guardó el producto pero hubo problems con la Imagen";
                    }
                }
            }

            return Json(new { successfulOp = successfulOp, generatedId = obj.ProductId, message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method that returns a base64 file if exists of given productId
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ProductImage(int productId)
        {
            bool conversion;
            Product oProduct = new BL_Product().GetProducts().Where(p => p.ProductId == productId).FirstOrDefault();

            string base64Text = BL_Resources.Base64Converter(Path.Combine(oProduct.ImageRoute, oProduct.ImageName), out conversion);

            return Json(new { conversion, base64Text, extension = Path.GetExtension(oProduct.ImageName) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to delete a Product from Admin page
        /// </summary>
        /// <param name="productId">product's Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteProduct(int productId)
        {
            bool response = false;
            string message = string.Empty;

            response = new BL_Product().DeleteProduct(productId, out message);

            return Json(new { result = response, message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}