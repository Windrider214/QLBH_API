using Newtonsoft.Json;
using QLBH_WebClient.DTO;
using QLBH_WebClient.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBH_WebClient.Controllers
{
    public class SanPhamController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";
        private static string accessToken = "";
        private static int totalRow = 0;

        public ActionResult PartialSanPham(string TenSp, int page, int pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(TenSp))
                {
                    List<SANPHAM> sp = new List<SANPHAM>();
                    var request_url = "/api/SanPham/GetSanPhamPaging";
                    Page pageData = new Page { page = page, pageSize = pageSize };
                    var jsonData = JsonConvert.SerializeObject(pageData);
                    var result = API_Interact.PullData(url_api, request_url, jsonData, "");
                    if (result.IsSuccessStatusCode)
                    {
                        //sp = new JavaScriptSerializer().Deserialize<List<SANPHAM>>(result);
                        sp = JsonConvert.DeserializeObject<List<SANPHAM>>(result.Content);
                        return PartialView(sp);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult PartialLoaiSP(string TenLoaiSP)
        {
            try
            {
                if (string.IsNullOrEmpty(TenLoaiSP))
                {
                    List<LOAISP> lsp = new List<LOAISP>();
                    var request_url = "/api/SanPham/GetLoaiSP";
                    var result = API_Interact.GetData(url_api, request_url, "");
                    if (!string.IsNullOrEmpty(result))
                    {
                        lsp = JsonConvert.DeserializeObject<List<LOAISP>>(result);
                        return PartialView(lsp);
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult PartialThuongHieu(string TenTh)
        {
            try
            {
                if (string.IsNullOrEmpty(TenTh))
                {
                    List<THUONGHIEU> th = new List<THUONGHIEU>();
                    var request_url = "/api/ThuongHieu/GetThuongHieu";
                    var result = API_Interact.GetData(url_api, request_url, "");
                    if (!string.IsNullOrEmpty(result))
                    {
                        th = JsonConvert.DeserializeObject<List<THUONGHIEU>>(result);
                        return PartialView(th);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(404, "Hệ thống không xác thực, xin mời đăng nhập lại !!!");
                    }
                }
                return null; 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: SanPham
        public ActionResult Index()
        {
            try
            {
                var request_url = "/api/SanPham/GetTotalRec";
                var result = API_Interact.GetData(url_api, request_url, "");
                ViewBag.TotalRow = Int32.Parse(result);
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Detail(string MaSP)
        {
            try
            {
                var sp = new SANPHAM();
                var request_url = "api/SanPham/GetSanPhamByIdClient";
                var result = API_Interact.GetDataById(url_api, request_url, MaSP, "maSp", "");
                if (result.IsSuccessStatusCode)
                {
                    sp = JsonConvert.DeserializeObject<SANPHAM>(result.Content);
                    return View(sp);
                }
                else
                {
                    return View("Bạn chưa đăng nhập hoặc đã hết thời gian đăng nhập !!!");
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult SearchSuggest(string TenSp)
        {
            try
            {
                List<SANPHAM> sp = new List<SANPHAM>();
                var request_url = "/api/SanPham/SearchSanPham";
                var result = API_Interact.SearchDataByName(url_api, request_url, TenSp, "TenSp", "");
                if (result.IsSuccessStatusCode)
                {
                    sp = JsonConvert.DeserializeObject<List<SANPHAM>>(result.Content);
                }
                return Json(result.Content, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}