using Newtonsoft.Json;
using QLBH_WebManage.Auth;
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
    public class UserController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:63228";

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel user)
        {
            var returnData = new ReturnData();
            try
            {

                //dynamic authenData = new {
                //    userName = userName,
                //    password = password 
                //};

                var requestData = new LoginModel { username = user.username, password = user.password };

                var jsonData = JsonConvert.SerializeObject(requestData);
                var request_url = "/api/Authenticate/login";
                var result = API_Interact.Auth(url_api, request_url, jsonData);

                if (!result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = result.Content;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Đăng nhập thất bại";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }                   
            }
            catch (Exception ex)
            {

                returnData.ResponseCode = -99;
                returnData.Description = ex.Message;
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckIn()
        {
            var returnData = new ReturnData();
            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/User/AuthCheck";
                    var result = API_Interact.PullData(url_api, request_url, "", jwtCookie.token);
                    if (result.IsSuccessStatusCode)
                    {
                        returnData.ResponseCode = 900;
                        returnData.Description = TokenDecode.GetID(jwtCookie.token);
                        return Json(returnData, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        returnData.ResponseCode = -600;
                        returnData.Description = "Thông tin đăng nhập không chính xác !!!";
                        return Json(returnData, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
    
            return null;
        }

        public ActionResult GetUser()
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
                        return PartialView(kh);
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