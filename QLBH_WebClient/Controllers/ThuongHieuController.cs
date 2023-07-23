using Newtonsoft.Json;
using QLBH_WebClient.DTO;
using QLBH_WebClient.Helper;
using QLBH_WebClient.Models;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBH_WebClient.Controllers
{
    public class ThuongHieuController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";

        // GET: ThuongHieu
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetProcTotalByBrand(string math)
        {
            try
            {
                var request_url = "/api/SanPham/GetTotalRecByBrand";
                var result = API_Interact.GetDataById(url_api, request_url, math, "math", "");
                return Json(result.Content, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Detail(string MaTh)
        {
            try
            {
                var th = new THUONGHIEU();
                var request_url = "api/ThuongHieu/GetThuongHieuById";
                var result = API_Interact.GetDataById(url_api, request_url, MaTh, "MaTh", "");
                if (result.IsSuccessStatusCode)
                {
                    th = JsonConvert.DeserializeObject<THUONGHIEU>(result.Content);
                    return View(th);

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}