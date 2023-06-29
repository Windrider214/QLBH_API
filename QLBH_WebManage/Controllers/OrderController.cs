using Newtonsoft.Json;
using QLBH_WebManage.DTO;
using QLBH_WebManage.Helper;
using QLBH_WebManage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private static int totalRow = 0;


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

        // GET: Order
        public ActionResult Index()
        {
            try
            {
                var request_url = "/api/Order/GetTotalRec";
                var result = API_Interact.GetData(url_api, request_url, "");
                ViewBag.TotalRow = Int32.Parse(result);
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}