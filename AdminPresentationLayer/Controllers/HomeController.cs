using BusinessLayer;
using ClosedXML.Excel;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.Mvc;

namespace AdminPresentationLayer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Users
        public ActionResult Users()
        {
            return View();
        }

        /// <summary>
        /// Method to get all users in db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUsers()
        {
            List<App_User> oList = new List<App_User>();
            oList = new BL_User().GetUsers();

            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to save an user in DB, Created or edited
        /// </summary>
        /// <param name="obj">Object with the user's data</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveUser(App_User obj)
        {
            object result;
            string message = string.Empty;

            if(obj.UserId == 0)
            {
                result = new BL_User().AddUser(obj, out message);
            }
            else
            {
                result = new BL_User().EditUser(obj, out message);
            }

            return Json(new { result, message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to delete an user from Admin page
        /// </summary>
        /// <param name="userId">user's Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteUser(int userId)
        {
            bool response = false;
            string message = string.Empty;

            response = new BL_User().DeleteUser(userId, out message);

            return Json(new { result = response, message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to get a sales report of given data
        /// </summary>
        /// <param name="beginDate">Min date to search to</param>
        /// <param name="endDate">Max date to search to</param>
        /// <param name="transactionId">Id of the transaction</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetSalesReport(string beginDate, string endDate, string transactionId)
        {
            List<Report> oList = new List<Report>();

            oList = new BL_Report().GetSalesReport(beginDate, endDate, transactionId);
            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to get the report for the dashboard
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult DashboardView()
        {
            Dashboard obj = new BL_Report().GetDashboardReport();
            return Json(new { result = obj }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to generate an Excel document with the Sale report the user searched
        /// </summary>
        /// <param name="beginDate">Min date searched</param>
        /// <param name="endDate">Max date searched</param>
        /// <param name="transactionId">Id of transaction</param>
        /// <returns></returns>
        [HttpPost]
        public FileResult ExportSale(string beginDate, string endDate, string transactionId)
        {
            List<Report> oList = new List<Report>();
            oList = new BL_Report().GetSalesReport(beginDate, endDate, transactionId);

            DataTable dt = new DataTable();

            dt.Locale = new CultureInfo("es-CO");
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("Id Transacción", typeof(string));

            foreach(Report rp in oList)
            {
                dt.Rows.Add(new object[] {
                    rp.SaleDate
                    ,rp.R_client
                    ,rp.R_product
                    ,rp.Price
                    ,rp.Amount
                    ,rp.Total
                    ,rp.TransactionId
                });
            }

            dt.TableName = "Datos";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }
    }
}