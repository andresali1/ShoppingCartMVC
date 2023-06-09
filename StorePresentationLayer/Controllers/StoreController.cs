using Antlr.Runtime.Misc;
using BusinessLayer;
using EntityLayer;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace StorePresentationLayer.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductDetail
        public ActionResult ProductDetail(int productId = 0)
        {
            Product oProduct = new Product();
            bool convertion;

            oProduct = new BL_Product().GetProducts().Where(p => p.ProductId == productId).FirstOrDefault();

            if(oProduct != null)
            {
                oProduct.Base64 = BL_Resources.Base64Converter(Path.Combine(oProduct.ImageRoute, oProduct.ImageName), out convertion);
                oProduct.Extension = Path.GetExtension(oProduct.ImageName);
            }

            return View(oProduct);
        }

        /// <summary>
        /// Method to get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CategoriesList()
        {
            List<Category> list = new List<Category>();

            list = new BL_Category().GetCategories();

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to get all brands by Id category
        /// </summary>
        /// <param name="categoryId">Id of the Category</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BrandListByCategory(int categoryId)
        {
            List<Brand> list = new List<Brand>();

            list = new BL_Brand().GetBrandsByCategory(categoryId);

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to get all products using filter
        /// </summary>
        /// <param name="categoryId">Id of the Category</param>
        /// <param name="brandId">Id of the Brand</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProducts(int categoryId, int brandId)
        {
            List<Product> list = new List<Product>();

            bool convertion;

            list = new BL_Product().GetProducts().Select(p => new Product()
            {
                ProductId = p.ProductId
                ,P_name = p.P_name
                ,P_description = p.P_description
                ,oBrand = p.oBrand
                ,oCategory = p.oCategory
                ,Price = p.Price
                ,Stock = p.Stock
                ,ImageRoute = p.ImageRoute
                ,Base64 = BL_Resources.Base64Converter(Path.Combine(p.ImageRoute, p.ImageName), out convertion)
                ,Extension = Path.GetExtension(p.ImageName)
                ,Active = p.Active
            }).Where(product =>
                product.oCategory.CategoryId == (categoryId == 0 ? product.oCategory.CategoryId : categoryId) &&
                product.oBrand.BrandId == (brandId == 0 ? product.oBrand.BrandId : brandId) &&
                product.Stock > 0 && product.Active == true
            ).ToList();

            var jsonResult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
    }
}