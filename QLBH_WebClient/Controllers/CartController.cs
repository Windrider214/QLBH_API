using Newtonsoft.Json;
using QLBH_WebClient.DTO;
using QLBH_WebClient.Helper;
using QLBH_WebClient.Models;
using QLBH_WebClient.Others;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using static QLBH_WebClient.Models.GHN;
using Microsoft.Ajax.Utilities;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Converters;

namespace QLBH_WebClient.Controllers
{
    public class CartController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";
        private static int slh = 0;

        //Mã vận đơn
        //private static string deliverCode = "";

        //Lưu tạm thông tin đơn hàng
        //private static OrderEditModel vnpayORDER = new OrderEditModel();
        //private static GHN codVNPAY = new GHN();


        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckOut()
        {
            var list = new List<Cart>();
            try
            {
                var cookie = Request.Cookies["ShoppingCart"] != null ? Request.Cookies["ShoppingCart"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    list = JsonConvert.DeserializeObject<List<Cart>>(HttpUtility.UrlDecode(cookie));
                    slh = list.Count();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return PartialView(list);
        }

        public ActionResult GetSLH()
        {
            var list = new List<Cart>();
            try
            {
                var cookie = Request.Cookies["ShoppingCart"] != null ? Request.Cookies["ShoppingCart"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    list = JsonConvert.DeserializeObject<List<Cart>>(HttpUtility.UrlDecode(cookie));
                    slh = list.Count();
                }
                else
                {
                    slh = 0;
                }    
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return Json(new { slh }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListProvince()
        {
            var options = new RestClientOptions("https://dev-online-gateway.ghn.vn")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/shiip/public-api/master-data/province", Method.Get);
            request.AddHeader("token", "5bac8d89-0acd-11ee-bb28-f6a6bf301a4e");
            RestResponse response = client.Execute(request);
            if(response.IsSuccessStatusCode)
            {
                var content = response.Content;
                return Json(content, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        public ActionResult GetListDistrict(int provinceID)
        {
            var options = new RestClientOptions("https://dev-online-gateway.ghn.vn")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/shiip/public-api/master-data/district", Method.Post);
            request.AddHeader("token", "5bac8d89-0acd-11ee-bb28-f6a6bf301a4e");
            request.AddHeader("Content-Type", "application/json");
            var data = new
            {
                province_id = provinceID
            };
            var body = JsonConvert.SerializeObject(data);
            request.AddJsonBody(body);
            RestResponse response = client.Execute(request);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                return Json(content, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        public ActionResult GetListWard(int districtID)
        {
            var options = new RestClientOptions("https://dev-online-gateway.ghn.vn")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/shiip/public-api/master-data/ward?district_id", Method.Post);
            request.AddHeader("token", "5bac8d89-0acd-11ee-bb28-f6a6bf301a4e");
            request.AddHeader("Content-Type", "application/json");
            var data = new
            {
                district_id = districtID
            };
            var body = JsonConvert.SerializeObject(data);
            request.AddJsonBody(body);
            RestResponse response = client.Execute(request);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                return Json(content, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        public ActionResult GetDeliverDate(int to_district_id, string to_ward_code)
        {
            var options = new RestClientOptions("https://dev-online-gateway.ghn.vn")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/shiip/public-api/v2/shipping-order/leadtime", Method.Post);
            request.AddHeader("token", "5bac8d89-0acd-11ee-bb28-f6a6bf301a4e");
            request.AddHeader("ShopId", "124702");
            request.AddHeader("Content-Type", "application/json");
            var data = new
            {
                from_district_id = 1489,
                from_ward_code = "1A0213",
                to_district_id = to_district_id,
                to_ward_code = to_ward_code,
                service_id = 53320
            };
            var body = JsonConvert.SerializeObject(data);
            request.AddJsonBody(body);
            RestResponse response = client.Execute(request);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                return Json(content, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        public ActionResult GetShippingFee(int provinceID, int districtID, string wardID, int TotalAmount)
        {
            var list = new List<Cart>();
            try
            {
                var cookie = Request.Cookies["ShoppingCart"] != null ? Request.Cookies["ShoppingCart"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    list = JsonConvert.DeserializeObject<List<Cart>>(HttpUtility.UrlDecode(cookie));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            var options = new RestClientOptions("https://dev-online-gateway.ghn.vn")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/shiip/public-api/v2/shipping-order/fee", Method.Post);
            request.AddHeader("token", "5bac8d89-0acd-11ee-bb28-f6a6bf301a4e");
            request.AddHeader("ShopId", "124702");
            request.AddHeader("Content-Type", "application/json");
            var data = new
            {
                from_district_id = 1489,
                service_id = 53320,
                to_district_id = districtID,
                to_ward_code = wardID,
                weight = list.Sum(x => x.soLuong) * 100,
                insurance_value = TotalAmount,
                cod_value = TotalAmount

            };
            var body = JsonConvert.SerializeObject(data);
            request.AddJsonBody(body);
            RestResponse response = client.Execute(request);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                return Json(content, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        public ActionResult OrderConfirm()
        {
            return View();
        }

        /// <summary>
        /// Lấy mã khách hàng để đưa vào đơn hàng
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

        /// <summary>
        /// Tạo đơn thanh toán bằng VNPAY
        /// </summary>
        /// <param name="thanhtien"></param>
        /// <returns></returns>
        public ActionResult Payment(int thanhtien, string ndCK)
        {
            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            DateTime ngaygiohientai = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone);

            string total = (thanhtien * 100).ToString();
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", total); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", ngaygiohientai.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", ndCK); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", ngaygiohientai.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            
            return Json(paymentUrl, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Tạo đơn hàng Giao hàng nhanh
        /// </summary>
        /// <returns></returns>
        public string CreateCODVNPAY()
        {
            var returnData = new ReturnData();
            try
            {
                var list = new List<Cart>();
                var cookie = Request.Cookies["ShoppingCart"] != null ? Request.Cookies["ShoppingCart"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    list = JsonConvert.DeserializeObject<List<Cart>>(HttpUtility.UrlDecode(cookie));
                }

                var codVNPAY = new GHN();
                var cookieCODVNPAY = Request.Cookies["CheckOutCODvnPay"] != null ? Request.Cookies["CheckOutCODvnPay"].Value : string.Empty;
                if (cookieCODVNPAY != null && !string.IsNullOrEmpty(cookieCODVNPAY))
                {
                    codVNPAY = JsonConvert.DeserializeObject<GHN>(HttpUtility.UrlDecode(cookieCODVNPAY));
                }

                //Thêm sản phẩm vào đơn hàng GHN
                List<Item> items = new List<Item>();
                var data = list;
                foreach (var proc in data)
                {
                    Item item = new Item();
                    item.name = proc.tenSp;
                    item.quantity = proc.soLuong;
                    item.price = proc.donGia;
                    items.Add(item);
                }
                codVNPAY.weight = list.Sum(x => x.soLuong) * 100;
                codVNPAY.items = items;

                var options = new RestClientOptions("https://dev-online-gateway.ghn.vn")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/shiip/public-api/v2/shipping-order/create", Method.Post);
                request.AddHeader("ShopId", "124702");
                request.AddHeader("token", "5bac8d89-0acd-11ee-bb28-f6a6bf301a4e");
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(codVNPAY);
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var ghnOrder = new GHNorder();
                    ghnOrder = JsonConvert.DeserializeObject<GHNorder>(content);
                    return ghnOrder.data.order_code;
                }
                else
                {
                    return "Chưa tạo được mã vận đơn !";
                }    
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult PaymentConfirm()
        {
            var returnData = new ReturnData();

            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        #region Tạo đơn hàng thanh toán bằng VNPAY
                        var vnpayORDER = new OrderEditModel();
                        var format = "dd/MM/yyyy";
                        var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
                        var cookieVNPAYORDER = Request.Cookies["CreateVNPAYOrder"] != null ? Request.Cookies["CreateVNPAYOrder"].Value : string.Empty;
                        if (cookieVNPAYORDER != null && !string.IsNullOrEmpty(cookieVNPAYORDER))
                        {
                            vnpayORDER = JsonConvert.DeserializeObject<OrderEditModel>(HttpUtility.UrlDecode(cookieVNPAYORDER));
                        }

                        //Create order
                        vnpayORDER.maVanDon = CreateCODVNPAY();
                        vnpayORDER.maHd = Guid.NewGuid().ToString();
                        vnpayORDER.maKh = GetMaKH();

                        //Create order detail
                        var list = new List<Cart>();
                        var cookie = Request.Cookies["ShoppingCart"] != null ? Request.Cookies["ShoppingCart"].Value : string.Empty;
                        if (cookie != null && !string.IsNullOrEmpty(cookie))
                        {
                            list = JsonConvert.DeserializeObject<List<Cart>>(HttpUtility.UrlDecode(cookie));
                        }

                        List<HOADONCT> items = new List<HOADONCT>();
                        foreach (var proc in list)
                        {
                            HOADONCT item = new HOADONCT();
                            item.maSp = proc.maSp;
                            item.soLuong = proc.soLuong;
                            item.donGiaBan = proc.donGia;
                            item.maHd = vnpayORDER.maHd;
                            items.Add(item);
                        }
                        vnpayORDER.OrderDetail = items;

                        var request_url = "/api/Order/InsertOrders";
                        var jsonData = JsonConvert.SerializeObject(vnpayORDER);
                        var result = API_Interact.InsertData(url_api, request_url, jsonData, "");
                        if (result.IsSuccessStatusCode)
                        {
                            returnData.ResponseCode = 600;
                            returnData.Description = "Thanh toán thành công hóa đơn VNPAY " + orderId + " của đơn hàng " + vnpayORDER.maHd + " | Mã giao dịch VNPAY : " + vnpayTranId + " | Mã vận đơn: " + vnpayORDER.maVanDon;

                            // Send email confirm
                            ClientConfirmEmail email = new ClientConfirmEmail();
                            email.userEmail = vnpayORDER.emailKh;
                            if(vnpayORDER.maVanDon == "400")
                            {
                                email.isCreatedOrder = false;
                                email.orderCode = vnpayORDER.maHd;
                            }
                            else
                            {
                                email.isCreatedOrder = true;
                                email.deliverCode = vnpayORDER.maVanDon;
                                email.orderCode = vnpayORDER.maHd;

                            }
                            SendEmailConfirmOrder(email);
                        }
                        return View(returnData);
                        #endregion
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode

                        returnData.ResponseCode = -600;
                        returnData.Description = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                        return View(returnData);
                    }
                }
                else
                {
                    returnData.ResponseCode = -900;
                    returnData.Description = "Có lỗi xảy ra trong quá trình xử lý";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }
            return Redirect("/TrangChu/Index");
        }


        /// <summary>
        /// Tạo đơn COD GHN
        /// </summary>
        /// <param name="ghn"></param>
        /// <returns></returns>
        public string PaymentCOD()
        {
            var returnData = new ReturnData();

            var list = new List<Cart>();
            var cookie = Request.Cookies["ShoppingCart"] != null ? Request.Cookies["ShoppingCart"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                list = JsonConvert.DeserializeObject<List<Cart>>(HttpUtility.UrlDecode(cookie));
            }

            var ghn = new GHN();
            var cookieCODGHN = Request.Cookies["CheckOutCODGHN"] != null ? Request.Cookies["CheckOutCODGHN"].Value : string.Empty;
            if (cookieCODGHN != null && !string.IsNullOrEmpty(cookieCODGHN))
            {
                ghn = JsonConvert.DeserializeObject<GHN>(HttpUtility.UrlDecode(cookieCODGHN));
            }

            var options = new RestClientOptions("https://dev-online-gateway.ghn.vn")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/shiip/public-api/v2/shipping-order/create", Method.Post);
            request.AddHeader("ShopId", "124702");
            request.AddHeader("token", "5bac8d89-0acd-11ee-bb28-f6a6bf301a4e");
            request.AddHeader("Content-Type", "application/json");

            //Add item to GHN order
            List<Item> items = new List<Item>();
            var data = list;
            foreach (var proc in data)
            {
                Item item = new Item();
                item.name = proc.tenSp;
                item.quantity = proc.soLuong;
                item.price = proc.donGia;
                items.Add(item);
            }
            ghn.weight = list.Sum(x => x.soLuong) * 100;
            ghn.items = items;

            var body = JsonConvert.SerializeObject(ghn);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = client.Execute(request);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                var ghnOrder = new GHNorder();
                ghnOrder = JsonConvert.DeserializeObject<GHNorder>(content);
                return  ghnOrder.data.order_code; //Lấy mã vận đơn sau khi tạo được đơn GHN
            }
            else
            {
                return "Chưa tạo được mã vận đơn !";
            }
        }

        // Tạo đơn hàng COD
        [HttpPost]
        public ActionResult CreateOrder(OrderEditModel order)
        {
            
            var returnData = new ReturnData();
            try
            {
                //Create order
                order.maVanDon = PaymentCOD();// Set mã vận đơn GHN cho đơn hàng
                order.maHd = Guid.NewGuid().ToString();
                order.maKh = GetMaKH();

                //Create order detail
                var list = new List<Cart>();
                var cookie = Request.Cookies["ShoppingCart"] != null ? Request.Cookies["ShoppingCart"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    list = JsonConvert.DeserializeObject<List<Cart>>(HttpUtility.UrlDecode(cookie));
                }

                List<HOADONCT> items = new List<HOADONCT>();
                foreach (var proc in list)
                {
                    HOADONCT item = new HOADONCT();
                    item.maSp = proc.maSp;
                    item.soLuong = proc.soLuong;
                    item.donGiaBan = proc.donGia;
                    item.maHd = order.maHd;
                    items.Add(item);
                }   
                order.OrderDetail = items;
                
                var request_url = "/api/Order/InsertOrders";
                var jsonData = JsonConvert.SerializeObject(order);
                var result = API_Interact.InsertData(url_api, request_url, jsonData, "");
                if (result.IsSuccessStatusCode)
                {

                    // Send email confirm
                    ClientConfirmEmail email = new ClientConfirmEmail();
                    email.userEmail = order.emailKh;
                    if(order.maVanDon == "400")
                    {
                        email.isCreatedOrder = false;
                        email.orderCode = order.maHd;
                        returnData.Description = @"Đơn hàng " + order.maHd + " được đặt thành công !!!";
                    }
                    else
                    {
                        email.isCreatedOrder = true;
                        email.deliverCode = order.maVanDon;
                        email.orderCode = order.maHd;
                        returnData.Description = @"Đơn hàng " + order.maHd + " được đặt thành công !!!" +
                                                "Quý khách có thể tra cứu đơn hàng với mã vận đơn '" + order.maVanDon + "' ";
                    }
                    SendEmailConfirmOrder(email);

                    returnData.ResponseCode = 600;
                   
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -900;
                    returnData.Description = "Đặt hàng thất bại";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Tạo email
        /// </summary>
        /// <param name="email"></param>
        public void SendEmailConfirmOrder(ClientConfirmEmail email)
        {
            var returnData = new ReturnData();

            try
            {
                var request_url = "/api/Order/SendEmailConfirmOrder";
                var jsonData = JsonConvert.SerializeObject(email);
                var result = API_Interact.InsertData(url_api, request_url, jsonData, "");
                if(result.IsSuccessStatusCode)
                {
                    ViewBag.Status = "OK";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Lấy thông tin cá nhân của tài khoản đã đăng nhập
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCustomerInform()
        {
            var returnData = new ReturnData();

            try
            {
                KHACHHANG kh = new KHACHHANG();
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/KhachHang/GetKhachHangByUserID";
                    var result = API_Interact.GetDataById(url_api, request_url, TokenDecode.GetID(jwtCookie.token), "userID", jwtCookie.token);
                    if (result.IsSuccessStatusCode)
                    {
                        kh = JsonConvert.DeserializeObject<KHACHHANG>(result.Content);
                        return Json(new KHACHHANG
                        {
                            maKh = kh.maKh,
                            tenKh = kh.tenKh,
                            sdt = kh.sdt,
                            diaChi = kh.diaChi,
                            emailKh = kh.emailKh,
                            loginId = kh.loginId
                        }, JsonRequestBehavior.AllowGet);
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