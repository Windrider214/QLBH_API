using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using QLBH_WebManage.DTO;
using QLBH_WebManage.Helper;
using QLBH_WebManage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QLBH_WebManage.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:63228";
        private static string accessToken = "";


        // GET: LoaiSanPham
        public ActionResult PartialIndex(string TenLoaiSP, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(TenLoaiSP))
                {
                    List<LOAISP> lsp = new List<LOAISP>();
                    var request_url = "/api/SanPham/GetLoaiSP";
                    var result = API_Interact.GetData(url_api, request_url, token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        accessToken = token;
                        lsp = JsonConvert.DeserializeObject<List<LOAISP>>(result);
                        return PartialView(lsp);
                    }
                }
                else
                {
                    List<LOAISP> lsp = new List<LOAISP>();
                    var request_url = "/api/SanPham/SearchLoaiSP";
                    var result = API_Interact.SearchDataByName(url_api, request_url, TenLoaiSP, "TenLoaiSP", token);
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        lsp = JsonConvert.DeserializeObject<List<LOAISP>>(result.Content);
                        return PartialView(lsp);

                    }
                }                 
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Delete(string MaLoai)
        {
            var returnData = new ReturnData();
            try
            {
                if (MaLoai == "")
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Không tìm thấy mã sản phẩm !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var request_url = "/api/SanPham/DeleteLoaiSP";
                var result = API_Interact.DeleteData(url_api, request_url, MaLoai, "MaLoai", accessToken);

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

        public ActionResult Detail(string MaLoai)
        {
            try
            {
                var lsp = new LOAISP();
                var request_url = "api/SanPham/GetLoaiSPById";
                var result = API_Interact.GetDataById(url_api, request_url, MaLoai , "MaLoai", accessToken);
                if (result.IsSuccessStatusCode)
                {
                    lsp = JsonConvert.DeserializeObject<LOAISP>(result.Content);
                    return View(lsp);

                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult LoaiSanPhamInsert(LOAISP lsp)
        {
            var returnData = new ReturnData();
            try
            {
                if (lsp.image == null)
                {
                    lsp.image = "";
                }
                else
                {
                    Image img = new Image { imageName = lsp.image, alias = lsp.tenLoaiSp, folderName = "categories" };
                    var saveImg_url = "/api/Image/SaveImage";
                    var imgData = JsonConvert.SerializeObject(img);
                    var imgString = API_Interact.SaveImage(media_api, saveImg_url, imgData);
                    if (!string.IsNullOrEmpty(imgString))
                    {
                        lsp.image = imgString.Replace("\"", "");
                    }
                }

                var request_url = "/api/SanPham/InsertLoaiSP";
                lsp.maLoai = Guid.NewGuid().ToString();
                var jsonData = JsonConvert.SerializeObject(lsp);
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
        public ActionResult LoaiSanPhamUpdate(LOAISP lsp)
        {
            var returnData = new ReturnData();
            try
            {
                if (CheckBase64.IsBase64String(lsp.image))
                {
                    Image img = new Image { imageName = lsp.image, alias = lsp.tenLoaiSp, folderName = "categories" };
                    var saveImg_url = "/api/Image/SaveImage";
                    var imgData = JsonConvert.SerializeObject(img);
                    var imgString = API_Interact.SaveImage(media_api, saveImg_url, imgData);
                    if (!string.IsNullOrEmpty(imgString))
                    {
                        lsp.image = imgString.Replace("\"", "");
                    }
                }


                var request_url = "/api/SanPham/UpdateLoaiSP";

                var jsonData = JsonConvert.SerializeObject(lsp);
                var result = API_Interact.UpdateData(url_api, request_url, jsonData, accessToken);

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
            catch (Exception)
            {

                throw;
            }
        }


    }
}