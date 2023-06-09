﻿using BusinessLayer;
using EntityLayer;
using StorePresentationLayer.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

            if (oProduct != null)
            {
                oProduct.Base64 = BL_Resources.Base64Converter(Path.Combine(oProduct.ImageRoute, oProduct.ImageName), out convertion);
                oProduct.Extension = Path.GetExtension(oProduct.ImageName);
            }

            return View(oProduct);
        }

        [SessionValidation]
        [Authorize]
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

            response = new BL_ShoppingCart().ManageProductCartAmount(clientId, productId, add, out message);

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

        /// <summary>
        /// Method to process payment
        /// </summary>
        /// <param name="oListShoppingCart">List of Shopping Cart Type object with data</param>
        /// <param name="oSale">Sale object type with Sale data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> PaymentProcess(List<Shopping_Cart> oListShoppingCart, Sale oSale)
        {
            decimal total = 0;

            DataTable sale_details = new DataTable();
            sale_details.Locale = new CultureInfo("es-CO");
            sale_details.Columns.Add("ProductId", typeof(string));
            sale_details.Columns.Add("Amount", typeof(int));
            sale_details.Columns.Add("Total", typeof(decimal));

            foreach(Shopping_Cart oCart in oListShoppingCart)
            {
                decimal subTotal = Convert.ToDecimal(oCart.Amount.ToString()) * oCart.oProduct.Price;

                total += subTotal;

                sale_details.Rows.Add(new object[]
                {
                    oCart.oProduct.ProductId,
                    oCart.Amount,
                    subTotal
                });
            }

            oSale.Total = total;
            oSale.ClientId = ((Client)Session["Client"]).ClientId;

            TempData["Sale"] = oSale;
            TempData["Sale_Details"] = sale_details;

            return Json(new { Status = true, Link = "/Store/PayDone?transactionId=code0001&status=true" }, JsonRequestBehavior.AllowGet);
        }

        [SessionValidation]
        [Authorize]
        // GET: PayDone
        public async Task<ActionResult> PayDone()
        {
            string transactionId = Request.QueryString["transactionId"];
            bool status = Convert.ToBoolean(Request.QueryString["status"]);

            ViewData["Status"] = status;

            if (status)
            {
                Sale oSale = (Sale)TempData["Sale"];

                DataTable sale_details = (DataTable)TempData["Sale_Details"];

                oSale.TransactionId = transactionId;

                string message = string.Empty;

                bool response = new BL_Sale().RegisterSale(oSale, sale_details, out message);

                ViewData["TransactionId"] = oSale.TransactionId;
            }

            return View();
        }

        [SessionValidation]
        [Authorize]
        // GET: MyShopping
        public ActionResult MyShopping()
        {
            int clientId = ((Client)Session["Client"]).ClientId;

            List<Sale_Details> oList = new List<Sale_Details>();

            bool conversion;

            oList = new BL_Sale().GetSaleRecord(clientId).Select(oc => new Sale_Details()
            {
                oProduct = new Product()
                {
                    P_name = oc.oProduct.P_name
                    ,Price = oc.oProduct.Price
                    ,Base64 = BL_Resources.Base64Converter(Path.Combine(oc.oProduct.ImageRoute, oc.oProduct.ImageName), out conversion)
                    ,Extension = Path.GetExtension(oc.oProduct.ImageName)
                }
                ,Amount = oc.Amount
                ,Total = oc.Total
                ,TransactionId = oc.TransactionId
            }).ToList();

            return View(oList);
        }
    }
}