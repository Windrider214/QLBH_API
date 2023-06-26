using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using QLBH_WebManage.DTO;
using QLBH_WebManage.Helper;
using QLBH_WebManage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QLBH_WebManage.Controllers
{
    public class ThuongHieuController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";
        private static string accessToken = "";

        public ActionResult PartialIndex(string TenTh, string token)
        {
            try
            {            
                if (string.IsNullOrEmpty(TenTh))
                {
                    List<THUONGHIEU> th = new List<THUONGHIEU>();
                    var request_url = "/api/ThuongHieu/GetThuongHieu";
                    var result = API_Interact.GetData(url_api, request_url, token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        accessToken = token;
                        th = JsonConvert.DeserializeObject<List<THUONGHIEU>>(result);
                        return PartialView(th);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(404, "Hệ thống không xác thực, xin mời đăng nhập lại !!!");
                    }   
                }
                else
                {
                    List<THUONGHIEU> th = new List<THUONGHIEU>();
                    var request_url = "/api/ThuongHieu/SearchThuongHieu";
                    var result = API_Interact.SearchDataByName(url_api, request_url, TenTh, "TenTh", token);
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        th = JsonConvert.DeserializeObject<List<THUONGHIEU>>(result.Content);
                        return PartialView(th);

                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: ThuongHieu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(string MaTh)
        {
            try
            {
                var th = new THUONGHIEU();
                var request_url = "api/ThuongHieu/GetThuongHieuById";
                var result = API_Interact.GetDataById(url_api, request_url, MaTh, "MaTh", accessToken);
                 if (result.IsSuccessStatusCode)
                {
                    th = JsonConvert.DeserializeObject<THUONGHIEU>(result.Content);
                    return View(th);

                }
                else
                {
                    return View("Bạn chưa đăng nhập hoặc đã hết thời gian đăng nhập !!!");
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Delete(string MaTH)
        {
            var returnData = new ReturnData();
            try
            {
                if (MaTH == "")
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Không tìm thấy mã thương hiệu !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var request_url = "/api/ThuongHieu/DeleteThuongHieu";
                var result = API_Interact.DeleteData(url_api, request_url, MaTH, "MaTH", accessToken);

                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 600;
                    returnData.Description = "Xóa thành công";
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    returnData.ResponseCode = -900;
                    returnData.Description = "Xóa thất bại";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }    

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThuongHieuInsert(THUONGHIEU th)
        {
            var returnData = new ReturnData();

            try
            {
                if (th.anhTh == null)
                {
                    th.anhTh = "";
                }
                else
                {
                    Image img = new Image { imageName = th.anhTh, alias = th.tenTh, folderName = "thuonghieu" };
                    var saveImg_url = "/api/Image/SaveImage";
                    var imgData = JsonConvert.SerializeObject(img);
                    var imgString = API_Interact.SaveImage(media_api, saveImg_url, imgData);
                    if (!string.IsNullOrEmpty(imgString))
                    {
                        th.anhTh = imgString.Replace("\"", "");
                    }
                }

                th.maTh = Guid.NewGuid().ToString();   
                var request_url = "/api/ThuongHieu/InsertThuongHieu";
                var jsonData = JsonConvert.SerializeObject(th);
                var result = API_Interact.InsertData(url_api, request_url, jsonData, accessToken);
                
                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Thêm thành công";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Thêm thất bại !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }    
   

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThuongHieuUpdate(THUONGHIEU th)
        {
            var returnData = new ReturnData();
            try
            {
                if (CheckBase64.IsBase64String(th.anhTh))
                {
                    Image img = new Image { imageName = th.anhTh, alias = th.tenTh, folderName = "thuonghieu" };
                    var saveImg_url = "/api/Image/SaveImage";
                    var imgData = JsonConvert.SerializeObject(img);
                    var imgString = API_Interact.SaveImage(media_api, saveImg_url, imgData);
                    if (!string.IsNullOrEmpty(imgString))
                    {
                        th.anhTh = imgString.Replace("\"", "");
                    }
                }
               
                var request_url = "/api/ThuongHieu/UpdateThuongHieu";
                var jsonData = JsonConvert.SerializeObject(th);
                var result = API_Interact.UpdateData(url_api, request_url, jsonData, accessToken);

                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Sửa thành công";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                returnData.ResponseCode = -1;
                returnData.Description = "Sửa thất bại !!!";

                return Json(returnData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}