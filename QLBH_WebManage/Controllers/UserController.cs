using Newtonsoft.Json;
using QLBH_WebManage.Auth;
using QLBH_WebManage.DTO;
using QLBH_WebManage.Helper;
using QLBH_WebManage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
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
            try
            {
                var request_url = "/api/User/GetTotalRec";
                var result = API_Interact.GetData(url_api, request_url, "");
                ViewBag.TotalRow = Int32.Parse(result);
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult SignIn()
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


                if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = result.Content;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else if (result.StatusCode == HttpStatusCode.OK)
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = result.Content;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = 0;
                    returnData.Description = result.Content;
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

        public ActionResult PartialIndex(string UserName, string token, int page, int pageSize)
        {
            var returnData = new ReturnData();

            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    if (string.IsNullOrEmpty(UserName))
                    {
                        List<USER> user = new List<USER>();
                        jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                        var request_url = "/api/User/GetAllUsersPaging";
                        Page pageData = new Page { page = page, pageSize = pageSize };
                        var jsonData = JsonConvert.SerializeObject(pageData);
                        var result = API_Interact.PullData(url_api, request_url, jsonData, jwtCookie.token);
                        if (result.IsSuccessStatusCode)
                        {
                            user = JsonConvert.DeserializeObject<List<USER>>(result.Content);
                            return PartialView(user);
                        }
                    }
                    else
                    {
                        List<USER> user = new List<USER>();
                        var request_url = "/api/User/SearchUser";
                        var result = API_Interact.SearchDataByName(url_api, request_url, UserName, "UserName", jwtCookie.token);
                        if (result.IsSuccessStatusCode)
                        {
                            user = JsonConvert.DeserializeObject<List<USER>>(result.Content);
                            return PartialView(user);
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

        public ActionResult LockUser(LockUser lckUser)
        {
            var returnData = new ReturnData();

            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Authenticate/lock-user";
                    var jsonData = JsonConvert.SerializeObject(lckUser);
                        var result = API_Interact.PullData(url_api, request_url, jsonData, jwtCookie.token);
                        if (result.IsSuccessStatusCode)
                        {
                            returnData.ResponseCode = 900;
                            returnData.Description = "Thay đổi thành công !!!";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            returnData.ResponseCode = -600;
                            returnData.Description = "Thay đổi thất bại !!!";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
                        }

                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult GetUserRole()
        {
            var returnData = new ReturnData();

            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    List<ROLE> roles = new List<ROLE>();
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/User/GetRole";
                    var result = API_Interact.GetData(url_api, request_url, jwtCookie.token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        roles = JsonConvert.DeserializeObject<List<ROLE>>(result);
                        return Json(roles, JsonRequestBehavior.AllowGet);
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Detail(string userID)
        {
            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    UserRoleById user = new UserRoleById();
                    var request_url = "api/Authenticate/get-userrole";
                    var result = API_Interact.GetDataById(url_api, request_url, userID, "userID", jwtCookie.token);
                    if (result.IsSuccessStatusCode)
                    {
                        user = JsonConvert.DeserializeObject<UserRoleById>(result.Content);
                        return PartialView(user);
                    }
                    else
                    {
                        return PartialView("Bạn chưa đăng nhập hoặc đã hết thời gian đăng nhập !!!");
                    }
                }             
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult UpdateRole(UserRoleById model)
        {
            var returnData = new ReturnData();

            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Authenticate/update-userrole";
                    var jsonData = JsonConvert.SerializeObject(model);
                    var result = API_Interact.PullData(url_api, request_url, jsonData, jwtCookie.token);
                        if (result.IsSuccessStatusCode)
                        {
                            returnData.ResponseCode = 900;
                            returnData.Description = "Thay đổi thành công !!!";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            returnData.ResponseCode = -600;
                            returnData.Description = "Thay đổi thất bại !!!";
                            return Json(returnData, JsonRequestBehavior.AllowGet);
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