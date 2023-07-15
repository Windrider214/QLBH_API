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
    public class KhachHangController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:63228";

        // GET: KhachHang
        public ActionResult Index()
        {
            try
            {
                var request_url = "/api/KhachHang/GetTotalRec";
                var result = API_Interact.GetData(url_api, request_url, "");
                ViewBag.TotalRow = Int32.Parse(result);
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult PartialIndex(string TenKH, string token, int page, int pageSize)
        {
            var returnData = new ReturnData();

            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    if (string.IsNullOrEmpty(TenKH))
                    {
                        List<KHACHHANG> kh = new List<KHACHHANG>();
                        jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                        var request_url = "/api/KhachHang/GetAllKhPaging";
                        Page pageData = new Page { page = page, pageSize = pageSize };
                        var jsonData = JsonConvert.SerializeObject(pageData);
                        var result = API_Interact.PullData(url_api, request_url, jsonData, jwtCookie.token);
                        if (result.IsSuccessStatusCode)
                        {
                            kh = JsonConvert.DeserializeObject<List<KHACHHANG>>(result.Content);
                            return PartialView(kh);
                        }
                    }
                    else
                    {
                        List<KHACHHANG> kh = new List<KHACHHANG>();
                        var request_url = "/api/KhachHang/SearchKH";
                        var result = API_Interact.SearchDataByName(url_api, request_url, TenKH, "TenKH", jwtCookie.token);
                        if (result.IsSuccessStatusCode)
                        {
                            kh = JsonConvert.DeserializeObject<List<KHACHHANG>>(result.Content);
                            return PartialView(kh);
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}