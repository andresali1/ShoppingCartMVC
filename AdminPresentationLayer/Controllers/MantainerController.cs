using BusinessLayer;
using EntityLayer;
using System.Collections.Generic;
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

        public ActionResult Brand()
        {
            return View();
        }

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
    }
}