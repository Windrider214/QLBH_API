using Newtonsoft.Json;
using QLBH_WebClient.DTO;
using QLBH_WebClient.Helper;
using QLBH_WebClient.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBH_WebClient.Controllers
{
    public class MediaController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";
        private static string accessToken = "";

        public ActionResult PartialIndex(string token)
        {
            try
            {
                    List<MEDIUM> med = new List<MEDIUM>();
                    var request_url = "/api/Media/GetMedia";
                    var result = API_Interact.GetData(url_api, request_url, token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        accessToken = token;
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

        
        public ActionResult PartialLogo()
        {
            try
            {
                MEDIUM med = new MEDIUM();
                var request_url = "/api/Media/GetLogo";
                var result = API_Interact.GetData(url_api, request_url, "");
                if (!string.IsNullOrEmpty(result))
                {
                    med = JsonConvert.DeserializeObject<MEDIUM>(result);
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

        // GET: Media
        public ActionResult Index()
        {
            return View();
        }

    }
}