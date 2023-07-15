using Newtonsoft.Json;
using QLBH_API.Helper;
using QLBH_WebClient.Auth;
using QLBH_WebClient.DTO;
using QLBH_WebClient.Helper;
using QLBH_WebClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLBH_WebClient.Controllers
{
    public class UserController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:63228";

        // GET: User
        public ActionResult SignIn()
        {
            var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                return null;

            }
            return View();
        }

        public ActionResult Login(LoginModel user)
        {
            var returnData = new ReturnData();
            try
            {
                var jsonData = JsonConvert.SerializeObject(user);
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

        public ActionResult Login2FA(Login2FA user)
        {
            var returnData = new ReturnData();
            try
            {

                var jsonData = JsonConvert.SerializeObject(user);
                var request_url = "/api/Authenticate/login-2FA";
                var result = API_Interact.Auth(url_api, request_url, jsonData);

                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = result.Content;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -1;
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

        public ActionResult Enable2FA(En2FA model)
        {
            var returnData = new ReturnData();

            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Authenticate/enable-2fa";
                    model.userID = TokenDecode.GetID(jwtCookie.token);
                    var jsonData = JsonConvert.SerializeObject(model);
                    var result = API_Interact.UpdateData(url_api, request_url, jsonData, jwtCookie.token);
                    if (result.IsSuccessStatusCode)
                    {
                        returnData.ResponseCode = 900;
                        returnData.Description = "Thay đổi thành công !!!";
                        return Json(returnData, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        returnData.ResponseCode = -600;
                        returnData.Description = "Lỗi xảy ra !!!";
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

        public ActionResult GetUserPanel()
        {
            var returnData = new ReturnData();

            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/User/GetUserById";
                    var result = API_Interact.GetDataById(url_api, request_url, TokenDecode.GetID(jwtCookie.token), "userID", jwtCookie.token);
                    if (result.IsSuccessStatusCode)
                    {
                        USER user = new USER();
                        user = JsonConvert.DeserializeObject<USER>(result.Content);
                        return PartialView(user);
                    }
                }
                return PartialView();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string GetCustomerID()
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
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult GetCustomer()
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

        public ActionResult Detail()
        {
            JwtCookie jwtCookie = new JwtCookie();
            var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                var request_url = "/api/User/GetUserById";
                var result = API_Interact.GetDataById(url_api, request_url, TokenDecode.GetID(jwtCookie.token), "userID", jwtCookie.token);
                if (result.IsSuccessStatusCode)
                {
                    USER user = new USER();
                    user = JsonConvert.DeserializeObject<USER>(result.Content);
                    return View(user);
                }
            }
            return View("Bạn chưa đăng nhập !!!");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateInform(KHACHHANG kh)
        {
            var returnData = new ReturnData();
            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/KhachHang/UpdateKhachHang";
                    var jsonData = JsonConvert.SerializeObject(kh);
                    var result = API_Interact.UpdateData(url_api, request_url, jsonData, jwtCookie.token);

                    if (result.IsSuccessStatusCode)
                    {
                        returnData.ResponseCode = 1;
                        returnData.Description = "Sửa thành công";
                        return Json(returnData, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        returnData.ResponseCode = -1;
                        returnData.Description = "Sửa thất bại !!!";
                        return Json(returnData, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new ReturnData { ResponseCode = -900, Description = "Có lỗi xảy ra !!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult GetUserOrder()
        {
            JwtCookie jwtCookie = new JwtCookie();
            var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);

                var request_url = "/api/Order/GetOrdersByCustomerID";
                var result = API_Interact.GetDataById(url_api, request_url, GetCustomerID(), "MaKH", jwtCookie.token);
                if (result.IsSuccessStatusCode)
                {
                    List<HOADON> hd = new List<HOADON>();
                    hd = JsonConvert.DeserializeObject<List<HOADON>>(result.Content);
                    return PartialView(hd);
                }
            }
            return PartialView();
        }

        public ActionResult GetListOrderProduct(string MaHD)
        {
            try
            {
                List<HOADONCT> hdct = new List<HOADONCT>();
                var request_url = "/api/Order/GetListOrderProduct";
                var result = API_Interact.GetDataById(url_api, request_url, MaHD, "maHd", "");
                if (result.IsSuccessStatusCode)
                {
                    hdct = JsonConvert.DeserializeObject<List<HOADONCT>>(result.Content);
                    return PartialView(hdct);

                }
                else
                {
                    return PartialView("Not found");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult OrderDetail(string MaHD)
        {
            try
            {
                var sp = new HOADON();
                var request_url = "/api/Order/GetOrdersByID";
                var result = API_Interact.GetDataById(url_api, request_url, MaHD, "maHd", "");
                if (result.IsSuccessStatusCode)
                {
                    sp = JsonConvert.DeserializeObject<HOADON>(result.Content);
                    return View(sp);

                }
                else
                {
                    return View("Lỗi xảy ra!!!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult ChangePass()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult UpdatePassword(ChangePasswordModel model)
        {
            var returnData = new ReturnData();
            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    model.userID = TokenDecode.GetID(jwtCookie.token);
                    var request_url = "/api/Authenticate/change-password";
                    var jsonData = JsonConvert.SerializeObject(model);
                    var result = API_Interact.UpdateData(url_api, request_url, jsonData, jwtCookie.token);

                    if (result.IsSuccessStatusCode)
                    {
                        returnData.ResponseCode = 1;
                        returnData.Description = "Đổi mật khẩu thành công";
                        return Json(returnData, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        returnData.ResponseCode = -1;
                        returnData.Description = "Đổi mật khẩu thất bại !!!";
                        return Json(returnData, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new ReturnData { ResponseCode = -900, Description = "Có lỗi xảy ra !!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult GetResetPassToken([Required] string email)
        {
            var returnData = new ReturnData();

            try
            {
                var request_url = "/api/Authenticate/gen-resetpasswordtoken";
                var result = API_Interact.GetDataById(url_api, request_url, email, "email", "");
                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 900;
                    returnData.Description = "Mã xác thực tìm mật khẩu đã gửi đến email của bạn !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Email không hợp lệ!!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {

                throw;

            }
        }

        public ActionResult ResetPassword(ForgotPassModel model)
        {
            var returnData = new ReturnData();

            try
            {
                var request_url = "/api/Authenticate/forgot-password";
                var jsonData = JsonConvert.SerializeObject(model);
                var result = API_Interact.InsertData(url_api, request_url, jsonData, "");
                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 900;
                    returnData.Description = "Đổi mật khẩu thành công !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = result.Content;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {

                throw;

            }
        }

        public string Register(RegisterModel model)
        {
            var returnData = new ReturnData();

            try
            {
                var request_url = "/api/Authenticate/register";
                var jsonData = JsonConvert.SerializeObject(model);
                var result = API_Interact.InsertData(url_api, request_url, jsonData, "");
                if (result.IsSuccessStatusCode)
                {
                    CreatedUser newUser = new CreatedUser();
                    newUser = JsonConvert.DeserializeObject<CreatedUser>(result.Content);
                    return newUser.newUserID;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {

                throw;

            }
        }

        public ActionResult CreateCustomer(SignUp sgup)
        {
            var returnData = new ReturnData();

            try
            {
                sgup.maKh = Guid.NewGuid().ToString();  
                RegisterModel reg = new RegisterModel
                {
                    Username = sgup.Username,
                    Password = sgup.Password,
                    Email = sgup.emailKh
                };

                sgup.loginId = Register(reg);

                if(sgup.loginId == string.Empty)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
                KHACHHANG kh = new KHACHHANG
                {
                    maKh = sgup.maKh,
                    tenKh = sgup.tenKh,
                    sdt = sgup.sdt,
                    diaChi = sgup.diaChi,
                    emailKh = sgup.emailKh,
                    loginId = sgup.loginId,
                };

                var request_url = "/api/KhachHang/InsertKhachHang";
                var jsonData = JsonConvert.SerializeObject(kh);
                var result = API_Interact.InsertData(url_api, request_url, jsonData, "");
                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 900;
                    returnData.Description = "Đăng kí thành công, hãy kiểm tra email để xác thực !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = result.Content;
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