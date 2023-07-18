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
    public class PhanHoiController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";

        // GET: PhanHoi
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lấy mã khách hàng 
        /// </summary>
        /// <returns></returns>
        public string GetMaKH()
        {
            var returnData = new ReturnData();

            try
            {

                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/KhachHang/GetKhachHangByUserID";
                    var result = API_Interact.GetDataById(url_api, request_url, TokenDecode.GetID(jwtCookie.token), "userID", jwtCookie.token);
                    if (result.IsSuccessStatusCode)
                    {
                        KHACHHANG kh = new KHACHHANG();
                        kh = JsonConvert.DeserializeObject<KHACHHANG>(result.Content);
                        return kh.maKh;
                    }
                }
                return "7ddc1b90-2c81-4c40-ad2c-7894dd2c2d8f";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult FeedbackInsert(PHANHOI ph)
        {
            var returnData = new ReturnData();
            try
            {
                ph.maPh = Guid.NewGuid().ToString();
                ph.maKh = GetMaKH();
                ph.NgayTraLoi = null;
                ph.phanHoi1 = "Đang chờ trả lời !";
                ph.TinhTrang = false;
                var request_url = "/api/PhanHoi/InsertFeedback";
                
                var jsonData = JsonConvert.SerializeObject(ph);
                var result = API_Interact.InsertData(url_api, request_url, jsonData, "");

                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Gửi phản hồi thành công";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Gửi phản hồi thất bại !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}