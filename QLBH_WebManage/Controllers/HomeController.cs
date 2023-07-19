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
    public class HomeController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:63228";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThongKeLoiNhuan()
        {
            try
            {
                List<object> data = new List<object>();
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Statistic/ThongKeLoiNhuan";
                    var result = API_Interact.GetData(url_api, request_url, jwtCookie.token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult ThongKeKinhDoanh()
        {
            try
            {
                List<object> data = new List<object>();
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Statistic/ThongKeKinhDoanh";
                    var result = API_Interact.GetData(url_api, request_url, jwtCookie.token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult ThongKeDoanhThuThang(string Year)
        {
            try
            {
                List<object> data = new List<object>();
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Statistic/ThongKeDoanhThuThang";
                    var result = API_Interact.GetDataById(url_api, request_url, Year , "year", jwtCookie.token);
                    if (result.IsSuccessStatusCode)
                    {
                        return Json(result.Content, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult ThongKeDoanhThu()
        {
            try
            {
                List<object> data = new List<object>();
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Statistic/ThongKeDoanhThu";
                    var result = API_Interact.GetData(url_api, request_url, jwtCookie.token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.Total = result;
                        return PartialView();
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult ThongKeDonHang()
        {
            try
            {
                List<object> data = new List<object>();
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Statistic/ThongKeDonHang";
                    var result = API_Interact.GetData(url_api, request_url, jwtCookie.token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.Total = result;
                        return PartialView();
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult ThongKeSanPham()
        {
            try
            {
                List<object> data = new List<object>();
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Statistic/ThongKeSanPham";
                    var result = API_Interact.GetData(url_api, request_url, jwtCookie.token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.Total = result;
                        return PartialView();
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult ThongKeKhachHang()
        {
            try
            {
                List<object> data = new List<object>();
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Statistic/ThongKeKhachHang";
                    var result = API_Interact.GetData(url_api, request_url, jwtCookie.token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.Total = result;
                        return PartialView();
                    }
                    else
                    {
                        return null;
                    }
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