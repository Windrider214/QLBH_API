using Newtonsoft.Json;
using QLBH_WebClient.DTO;
using QLBH_WebClient.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBH_WebClient.Controllers
{
    public class TrangChuController : Controller
    {

        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";
        private static string accessToken = "";

        public ActionResult PartialBanner()
        {
            try
            {
                List<MEDIUM> med = new List<MEDIUM>();
                var request_url = "/api/Media/GetBanner";
                var result = API_Interact.GetData(url_api, request_url, "");
                if (!string.IsNullOrEmpty(result))
                {
                    med = JsonConvert.DeserializeObject<List<MEDIUM>>(result);
                    return PartialView(med);
                }
                else
                {
                    return new HttpStatusCodeResult(404, "Hệ thống không xác thực, xin mời đăng nhập lại !!!");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult PartialLoaiSP()
        {
            try
            {
                    List<LOAISP> lsp = new List<LOAISP>();
                    var request_url = "/api/SanPham/GetLoaiSP";
                    var result = API_Interact.GetData(url_api, request_url, "");
                    if (!string.IsNullOrEmpty(result))
                    {
                        lsp = JsonConvert.DeserializeObject<List<LOAISP>>(result);
                        return PartialView(lsp);

                    }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult PartialTopSP()
        {
            try
            {
                List<SANPHAM> sp = new List<SANPHAM>();
                var request_url = "/api/SanPham/GetTop5";
                var result = API_Interact.GetData(url_api, request_url, "");
                if (!string.IsNullOrEmpty(result))
                {
                    sp = JsonConvert.DeserializeObject<List<SANPHAM>>(result);
                    return PartialView(sp);

                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult PartialBlogSaleNew()
        {
            try
            {
                BLOG sp = new BLOG();
                var request_url = "/api/Blog/GetBlogSaleNews";
                var result = API_Interact.GetData(url_api, request_url, "");
                if (!string.IsNullOrEmpty(result))
                {
                    sp = JsonConvert.DeserializeObject<BLOG>(result);
                    return PartialView(sp);

                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: TrangChu
        public ActionResult Index()
        {
            return View();
        }
    }
}