using Newtonsoft.Json;
using QLBH_WebManage.DTO;
using QLBH_WebManage.Helper;
using QLBH_WebManage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBH_WebManage.Controllers
{
    public class OrderController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";
        private static string accessToken = "";
        private static int totalRowByDate = 0;


        public ActionResult PartialIndex(string maHd, string token, int page, int pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(maHd))
                {
                    List<HOADON> hd = new List<HOADON>();
                    var request_url = "/api/Order/GetOrdersPaging";
                    Page pageData = new Page { page = page, pageSize = pageSize };
                    var jsonData = JsonConvert.SerializeObject(pageData);
                    var result = API_Interact.PullData(url_api, request_url, jsonData, token);
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        //sp = new JavaScriptSerializer().Deserialize<List<SANPHAM>>(result);
                        hd = JsonConvert.DeserializeObject<List<HOADON>>(result.Content);
                        return PartialView(hd);

                    }
                }
                else
                {
                    List<HOADON> hd = new List<HOADON>();
                    var request_url = "/api/Order/SearchOrder";
                    var result = API_Interact.SearchDataByName(url_api, request_url, maHd, "maHd", token);
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        hd = JsonConvert.DeserializeObject<List<HOADON>>(result.Content);
                        return PartialView(hd);

                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult Delete(string maHd)
        {
            var returnData = new ReturnData();
            try
            {
                if (maHd == "")
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Không tìm thấy mã hóa đơn !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var request_url = "/api/Order/DeleteOrder";
                var result = API_Interact.DeleteData(url_api, request_url, maHd, "maHd", accessToken);

                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 600;
                    returnData.Description = "Xóa thành công";
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    returnData.ResponseCode = -900;
                    returnData.Description = "Xóa thất bại";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult OrderUpdate(HOADON hd)
        {
            var returnData = new ReturnData();
            try
            {

                var request_url = "/api/Order/UpdateOrder";

                var jsonData = JsonConvert.SerializeObject(hd);
                var result = API_Interact.UpdateData(url_api, request_url, jsonData, accessToken);

                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Cập nhật đơn hàng thành công !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Cập nhật đơn hàng thất bại !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Order
        public ActionResult Index()
        {
            try
            {
                var request_url = "/api/Order/GetTotalRec";
                var result = API_Interact.GetData(url_api, request_url, "");
                ViewBag.TotalRow = Int32.Parse(result);
                ViewBag.TotalRowBydDate = Int32.Parse(totalRowByDate.ToString());
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Detail(string MaHD)
        {
            try
            {
                var sp = new HOADON();
                var request_url = "/api/Order/GetOrdersByID";
                var result = API_Interact.GetDataById(url_api, request_url, MaHD, "maHd", accessToken);
                if (result.IsSuccessStatusCode)
                {
                    sp = JsonConvert.DeserializeObject<HOADON>(result.Content);
                    return View(sp);

                }
                else
                {
                    return View("Lỗi xảy ra!!!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public ActionResult GetListOrderProduct(string MaHD)
        {
            try
            {
                List<HOADONCT> hdct = new List<HOADONCT>();
                var request_url = "/api/Order/GetListOrderProduct";
                var result = API_Interact.GetDataById(url_api, request_url, MaHD, "maHd", accessToken);
                if (result.IsSuccessStatusCode)
                {
                    hdct = JsonConvert.DeserializeObject<List<HOADONCT>>(result.Content);
                    return PartialView(hdct);

                }
                else
                {
                    return PartialView("Not found");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult GetTotalByDate(DateRange dr)
        {
            var returnData = new ReturnData();
            try
            {
                List<HOADON> TotalRowByDate = new List<HOADON>();
                var request_url1 = "/api/Order/GetListByDate";
                var jsonData1 = JsonConvert.SerializeObject(dr);
                var result1 = API_Interact.PullData(url_api, request_url1, jsonData1, accessToken);
                if (result1.IsSuccessStatusCode)
                {
                    TotalRowByDate = JsonConvert.DeserializeObject<List<HOADON>>(result1.Content);
                    returnData.ResponseCode = TotalRowByDate.Count();
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
    
        }


        public ActionResult GetListPagingByDate(OrderByDate order)
        {
            try
            {
                    List<HOADON> hd = new List<HOADON>();
                    var request_url = "/api/Order/GetListPagingByDate";
                    var jsonData = JsonConvert.SerializeObject(order);
                    var result = API_Interact.PullData(url_api, request_url, jsonData, accessToken);
                    if (result.IsSuccessStatusCode)
                    {
                        
                        //sp = new JavaScriptSerializer().Deserialize<List<SANPHAM>>(result);
                        hd = JsonConvert.DeserializeObject<List<HOADON>>(result.Content);
                        return PartialView(hd);

                    }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}