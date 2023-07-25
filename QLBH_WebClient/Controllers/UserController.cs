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

        // Tạo trang đăng nhập
        public ActionResult SignIn()
        {
            var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                return View("Vui lòng đăng xuất tài khoản !!!");

            }
            return View();
        }

        // Xác nhận email thành công
        public ActionResult ConfirmRegister()
        {
            var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                return null;

            }
            return View();
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult Login(LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

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


        /// <summary>
        /// Đăng nhập với xác thực 2 yếu tố
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Bật xác thực 2 yếu tố
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Tạo đường dẫn panel của user sau khi đăng nhập
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Lấy mã khách hàng của tài khoản
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Lấy ra thông tin cá nhân của tài khoản 
        /// </summary>
        /// <returns></returns>
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
            USER user = new USER();
            JwtCookie jwtCookie = new JwtCookie();
            var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                var request_url = "/api/User/GetUserById";
                var result = API_Interact.GetDataById(url_api, request_url, TokenDecode.GetID(jwtCookie.token), "userID", jwtCookie.token);
                if (result.IsSuccessStatusCode)
                {
                    user = JsonConvert.DeserializeObject<USER>(result.Content);
                    ViewBag.TotalRowOrder = GetCusTotalOrder();
                    ViewBag.TotalRowFeedback = GetCusTotalFeedback();
                    return View(user);
                }
                user.id = "";
                return View(user);
            }
            return Redirect("~/Views/TrangChu/Index");
        }
        


        /// <summary>
        /// Thay đổi thông tin cá nhân
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
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

        #region Lấy thông tin đơn hàng của tài khoản
        //Lấy tổng số đơn ( để phân trang )
        public int GetCusTotalOrder()
        {
            JwtCookie jwtCookie = new JwtCookie();
            var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                var request_url = "/api/Order/GetCusTotalOrder";
                var result = API_Interact.GetDataById(url_api, request_url, GetCustomerID(), "MaKH", jwtCookie.token);
                if (result.IsSuccessStatusCode)
                {
                    return int.Parse(result.Content);
                }
            }
            return 1;
        }

        // Lấy đơn hàng
        public ActionResult GetUserOrder(CusOrder order)
        {
            JwtCookie jwtCookie = new JwtCookie();
            var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                order.MaKH = GetCustomerID();
                var request_url = "/api/Order/GetListPagingByCusID";
                var jsonData = JsonConvert.SerializeObject(order);
                var result = API_Interact.PullData(url_api, request_url, jsonData, jwtCookie.token);
                if (result.IsSuccessStatusCode)
                {
                    List<HOADON> hd = new List<HOADON>();
                    hd = JsonConvert.DeserializeObject<List<HOADON>>(result.Content);
                    return PartialView(hd);
                }
            }
            return PartialView();
        }

        // lấy sản phẩm trong đơn ( HOADONCT )
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

        // Lấy thông tin đơn hàng
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
        #endregion

        #region Đổi mật khẩu trong user panel
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
        #endregion

        #region Tạo mới tài khoản

        /// <summary>
        /// Khi đăng ký mới tài khoản thì đồng thời ghi lại thông tin cá nhân
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

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

                if (sgup.loginId == string.Empty)
                {

                    returnData.ResponseCode = -700;
                    returnData.Description = "Email hoặc tên tài khoản đã sử dụng !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
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
        #endregion

        #region Lấy thông tin phản hồi
        //Lấy tổng số phản hồi ( để phân trang )
        public int GetCusTotalFeedback()
        {
            JwtCookie jwtCookie = new JwtCookie();
            var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                var request_url = "/api/PhanHoi/GetCusTotalFeedback";
                var result = API_Interact.GetDataById(url_api, request_url, GetCustomerID(), "MaKH", jwtCookie.token);
                if (result.IsSuccessStatusCode)
                {
                    return int.Parse(result.Content);
                }
            }
            return 1;
        }

        // Lấy phản hồi
        public ActionResult GetUserFeedback(CusFeedback fb)
        {
            JwtCookie jwtCookie = new JwtCookie();
            var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                fb.MaKH = GetCustomerID();
                var request_url = "/api/PhanHoi/GetListPagingByCusID";
                var jsonData = JsonConvert.SerializeObject(fb);
                var result = API_Interact.PullData(url_api, request_url, jsonData, jwtCookie.token);
                if (result.IsSuccessStatusCode)
                {
                    List<PHANHOI> ph = new List<PHANHOI>();
                    ph = JsonConvert.DeserializeObject<List<PHANHOI>>(result.Content);
                    return PartialView(ph);
                }
            }
            return PartialView();
        }

        // Xem nội dung trả lời
        public ActionResult FeedBackDetail(string MaPH)
        {
            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var ph = new PHANHOI();
                    var request_url = "/api/PhanHoi/GetFeedbackByID";
                    var result = API_Interact.GetDataById(url_api, request_url, MaPH, "MaPH", jwtCookie.token);
                    if (result.IsSuccessStatusCode)
                    {
                        ph = JsonConvert.DeserializeObject<PHANHOI>(result.Content);
                        return PartialView(ph);

                    }
                }
                return PartialView();

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}