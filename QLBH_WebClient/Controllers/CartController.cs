﻿using Newtonsoft.Json;
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

namespace QLBH_WebClient.Controllers
{
    public class CartController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";
        private static int slh = 0;
        private static int total = 0;
        private static string deliverCode = "";

        public List<Cart> Carts
        {
            get
            {
                var data = (List<Cart>)HttpContext.Session["GioHang"];
                if (data == null)
                {
                    data = new List<Cart>();
                }
                return data;
            }
        }
    
        public ActionResult PartialIndex()
        {
            ViewBag.Total = total;
            return PartialView(Carts);
        }

        public ActionResult RemoveItem(string masp)
        {
            var returnData = new ReturnData();
            var delItem = Carts.Where(x => x.maSp == masp).FirstOrDefault();
            if(delItem != null)
            {
                Carts.Remove(delItem);
                //total = Int32.Parse(Carts.Sum(x => x.ThanhTien).ToString());
            }

                return Json( new { SoLuong = Carts.Count }, JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult AddToCart(string masp, int SoLuong, string type = "Normal")
        {
            var myCart = Carts;
            var item = myCart.Where(x => x.maSp == masp).FirstOrDefault();

            if (item == null)//chưa có
            {
                var sp = new SANPHAM();
                var request_url = "api/SanPham/GetSanPhamByIdClient";
                var result = API_Interact.GetDataById(url_api, request_url, masp, "maSp", "");
                if (result.IsSuccessStatusCode)
                {
                    sp = JsonConvert.DeserializeObject<SANPHAM>(result.Content);
                    item = new Cart
                    {
                        maSp = masp,
                        tenSp = sp.tenSp,
                        donGia = sp.donGia,
                        soLuong = SoLuong,
                        seoImage = sp.seoImage
                    };

                }
 
                myCart.Add(item);
                
            }
            else
            {
                item.soLuong += SoLuong;
            }
            Session["GioHang"] = myCart;
            slh = Carts.Count();
            //total = Int32.Parse(Carts.Sum(x => x.ThanhTien).ToString());
            if (type == "ajax")
            {
                return Json(new
                {
                    SoLuong = Carts.Count
                }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");
        }


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

        public ActionResult PaymentCOD(GHN ghn)
        {
            var returnData = new ReturnData();

            var list = new List<Cart>();
         
            var cookie = Request.Cookies["ShoppingCart"] != null ? Request.Cookies["ShoppingCart"].Value : string.Empty;
            if (cookie != null && !string.IsNullOrEmpty(cookie))
            {
                    list = JsonConvert.DeserializeObject<List<Cart>>(HttpUtility.UrlDecode(cookie));
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
            foreach (var proc in data) {
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
                deliverCode = ghnOrder.data.order_code;
            }
            return Json( new ReturnData { ResponseCode = 900, Description = "Success"}, JsonRequestBehavior.AllowGet); 

        }

        public ActionResult Payment(int thanhtien)
        {
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
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            
            return Json(paymentUrl, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PaymentConfirm()
        {
            return View();
        }
        
        public ActionResult CreateOrder(OrderEditModel order)
        {
            var returnData = new ReturnData();
            try
            {
                //Create order
                order.maVanDon = deliverCode;
                order.maHd = Guid.NewGuid().ToString();
                order.maKh = "7ddc1b90-2c81-4c40-ad2c-7894dd2c2d8f";

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
                    returnData.ResponseCode = 600;
                    returnData.Description = "Đặt hàng thành công";

                    // Send email confirm
                    ClientConfirmEmail email = new ClientConfirmEmail();
                    email.userEmail = order.emailKh;
                    email.deliverCode = order.maVanDon;
                    SendEmailConfirmOrder(email);

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
                    ViewBag.Check = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}