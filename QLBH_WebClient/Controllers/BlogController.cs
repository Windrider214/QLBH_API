using Newtonsoft.Json;
using QLBH_WebClient.DTO;
using QLBH_WebClient.Helper;
using QLBH_WebClient.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLBH_WebClient.Controllers
{
    public class BlogController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";
        private static string accessToken = "";
        private static int totalRow = 0;

        // GET: Blog
        public ActionResult Index()
        {
            try
            {
                var request_url = "/api/Blog/GetTotalRec";
                var result = API_Interact.GetData(url_api, request_url, "");
                if (!string.IsNullOrEmpty(result))
                {
                    ViewBag.TotalRow = Int32.Parse(result);
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult PartialBlogType()
        {
            try
            {

                List<BLOGTYPE> blgtype = new List<BLOGTYPE>();
                var request_url = "/api/Blog/GetBlogType";
                var result = API_Interact.GetData(url_api, request_url, "");
                if (!string.IsNullOrEmpty(result))
                {
                    blgtype = JsonConvert.DeserializeObject<List<BLOGTYPE>>(result);
                    var item = blgtype.SingleOrDefault(x => x.maDm == "32db0063-ad1a-4036-a410-99de23323211");
                    if(item != null)
                    {
                        blgtype.Remove(item);
                    }
                    return PartialView(blgtype);
                }
               

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult PartialIndex(string MaTinTuc, string token, int page, int pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(MaTinTuc))
                {
                    List<BLOG> blg = new List<BLOG>();
                    var request_url = "/api/Blog/GetBlogPaging";
                    Page pageData = new Page { page = page, pageSize = pageSize };
                    var jsonData = JsonConvert.SerializeObject(pageData);
                    var result = API_Interact.PullData(url_api, request_url, jsonData, token);
                    if (result.IsSuccessStatusCode)
                    {
                        blg = JsonConvert.DeserializeObject<List<BLOG>>(result.Content);
                        var item = blg.SingleOrDefault(x => x.maDm == "32db0063-ad1a-4036-a410-99de23323211");
                        blg.Remove(item);
                        return PartialView(blg);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(404, "Không tìm thấy !!!");
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult Detail(string MaTinTuc)
        {
            try
            {
                var blg = new BLOG();
                var request_url = "api/Blog/GetByIdClient";
                var result = API_Interact.GetDataById(url_api, request_url, MaTinTuc, "MaTinTuc", "");
                if (result.IsSuccessStatusCode)
                {
                    blg = JsonConvert.DeserializeObject<BLOG>(result.Content);
                    return View(blg);

                }
                else
                {
                    return View("Không tìm thấy !!!");
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult GetBlogTotalByType(string madm)
        {
            try
            {
                var request_url = "/api/Blog/GetTotalByType";
                var result = API_Interact.GetDataById(url_api, request_url, madm, "madm", "");
                return Json(result.Content, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult PartialBlogByType(BlogFilter blg)
        {
            try
            {
                List<BLOG> sp = new List<BLOG>();
                var request_url = "/api/Blog/GetBlogPagingByType";
                var jsonData = JsonConvert.SerializeObject(blg);
                var result = API_Interact.PullData(url_api, request_url, jsonData, "");
                if (result.IsSuccessStatusCode)
                {
                    //sp = new JavaScriptSerializer().Deserialize<List<SANPHAM>>(result);
                    sp = JsonConvert.DeserializeObject<List<BLOG>>(result.Content);
                    return PartialView(sp);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhatLuotXem(string MaTinTuc)
        {
            try
            {

                var request_url = "/api/Blog/CapNhatLuotXem";
                var result = API_Interact.GetDataById(url_api, request_url, MaTinTuc, "MaTinTuc", "");
                if(result.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK);

                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult BlogAbout()
        {
            try
            {
                BLOG sp = new BLOG();
                var request_url = "/api/Blog/GetAbout";
                var result = API_Interact.GetData(url_api, request_url, "");
                if (!string.IsNullOrEmpty(result))
                {
                    sp = JsonConvert.DeserializeObject<BLOG>(result);
                    return View(sp);

                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult HuongDan()
        {
            return View();
        }
    }
}