using BusinessLayer;
using EntityLayer;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;

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

            if (oProduct != null)
            {
                oProduct.Base64 = BL_Resources.Base64Converter(Path.Combine(oProduct.ImageRoute, oProduct.ImageName), out convertion);
                oProduct.Extension = Path.GetExtension(oProduct.ImageName);
            }

            return View(oProduct);
        }

        // GET: ShoppingCart
        public ActionResult ShoppingCart()
        {
            return View();
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
                , P_name = p.P_name
                , P_description = p.P_description
                , oBrand = p.oBrand
                , oCategory = p.oCategory
                , Price = p.Price
                , Stock = p.Stock
                , ImageRoute = p.ImageRoute
                , Base64 = BL_Resources.Base64Converter(Path.Combine(p.ImageRoute, p.ImageName), out convertion)
                , Extension = Path.GetExtension(p.ImageName)
                , Active = p.Active
            }).Where(product =>
                product.oCategory.CategoryId == (categoryId == 0 ? product.oCategory.CategoryId : categoryId) &&
                product.oBrand.BrandId == (brandId == 0 ? product.oBrand.BrandId : brandId) &&
                product.Stock > 0 && product.Active == true
            ).ToList();

            var jsonResult = Json(new { data = list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        /// <summary>
        /// Method to add a product to one client's shopping cart
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddToCart(int productId)
        {
            int clientId = ((Client)Session["Client"]).ClientId;

            bool exists = new BL_ShoppingCart().CartExists(clientId, productId);

            bool response = false;

            string message = string.Empty;

            if (exists)
            {
                message = "El producto ya existe en el carrito";
            }
            else
            {
                response = new BL_ShoppingCart().ManageProductCartAmount(clientId, productId, true, out message);
            }

            return Json(new { response, message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to get the amount of products in a client's shopping cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult AmountInCart()
        {
            int clientId = ((Client)Session["Client"]).ClientId;
            int amount = new BL_ShoppingCart().GetShoppingCartAmount(clientId);

            return Json(new { amount }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to get all products in a Client's ShoppingCart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetShoppingCartProducts()
        {
            int clientId = ((Client)Session["Client"]).ClientId;

            List<Shopping_Cart> oList = new List<Shopping_Cart>();

            bool conversion;

            oList = new BL_ShoppingCart().GetProductsInCart(clientId).Select(oc => new Shopping_Cart()
            {
                oProduct = new Product()
                {
                    ProductId = oc.oProduct.ProductId,
                    P_name = oc.oProduct.P_name,
                    oBrand = oc.oProduct.oBrand,
                    Price = oc.oProduct.Price,
                    ImageRoute = oc.oProduct.ImageRoute,
                    Base64 = BL_Resources.Base64Converter(Path.Combine(oc.oProduct.ImageRoute, oc.oProduct.ImageName), out conversion),
                    Extension = Path.GetExtension(oc.oProduct.ImageName)
                },
                Amount = oc.Amount
            }).ToList();

            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to add o substract products in a Client's product cart
        /// </summary>
        /// <param name="productId">Id of the Product</param>
        /// <param name="add">Is adding (true - false)</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ManageProductCartAmount(int productId, bool add)
        {
            int clientId = ((Client)Session["Client"]).ClientId;

            bool response = false;

            string message = string.Empty; 

            response = new BL_ShoppingCart().ManageProductCartAmount(clientId, productId, true, out message);

            return Json(new { response, message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to delete a product from Client's shopping cart
        /// </summary>
        /// <param name="productId">Id of the Product</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteCartProduct(int productId)
        {
            int clientId = ((Client)Session["Client"]).ClientId;

            bool response = false;

            string message = string.Empty;

            response = new BL_ShoppingCart().DeleteCartProduct(clientId, productId);

            return Json(new { response, message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to gel all the departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetDepartmentList()
        {
            List<Department> oList = new List<Department>();

            oList = new BL_Location().GetDepartmentList();

            //return Json(new { list = oList }, JsonRequestBehavior.AllowGet);
            return Json(new { list = oList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to get all the towns by Department Id
        /// </summary>
        /// <param name="departmentId">Id of the department</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTownListByDepartmentId(int departmentId)
        {
            List<Town> oList = new List<Town>();

            oList = new BL_Location().GetTownListByDepartmentId(departmentId);

            return Json(new { list = oList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to get all the Town location by Town Id
        /// </summary>
        /// <param name="townId">Id of the Town</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTownLocationListByTownId(int townId)
        {
            List<Town_Location> oList = new List<Town_Location>();

            oList = new BL_Location().GetTownLocationListByTownId(townId);

            return Json(new { list = oList }, JsonRequestBehavior.AllowGet);
        }
    }
}