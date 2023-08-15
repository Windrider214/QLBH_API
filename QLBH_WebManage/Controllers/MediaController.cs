using Newtonsoft.Json;
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

        public ActionResult GetMediaType(string token)
        {
            try
            {
                List<MEDIATYPE> medtype = new List<MEDIATYPE>();
                var request_url = "/api/Media/GetMediaType";
                var result = API_Interact.GetData(url_api, request_url, token);
                if (!string.IsNullOrEmpty(result))
                {
                    medtype = JsonConvert.DeserializeObject<List<MEDIATYPE>>(result);
                }
                return Json(medtype, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Delete(string MediaId)
        {
            var returnData = new ReturnData();
            try
            {
                if (MediaId == "")
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Không tìm thấy mã media !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var request_url = "/api/Media/DeleteMedia";
                var result = API_Interact.DeleteData(url_api, request_url, MediaId, "MediaId", accessToken);

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
        public ActionResult MediaInsert(MEDIUM med)
        {
            var returnData = new ReturnData();
            try
            {
                if (med.image == null)
                {
                    med.image = "";
                }
                else
                {
                    Image img = new Image { imageName = med.image, alias = "media", folderName = "media" };
                    var saveImg_url = "/api/Image/SaveImage";
                    var imgData = JsonConvert.SerializeObject(img);
                    var imgString = API_Interact.SaveImage(media_api, saveImg_url, imgData);
                    if (!string.IsNullOrEmpty(imgString))
                    {
                        med.image= imgString.Replace("\"", "");
                    }
                }

                med.mediaId = Guid.NewGuid().ToString();
                var request_url = "/api/Media/InsertMedia";
                var jsonData = JsonConvert.SerializeObject(med);
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

        public ActionResult LockMedia(LockMedia lckMedia)
        {
            var returnData = new ReturnData();

            try
            {
                JwtCookie jwtCookie = new JwtCookie();
                var cookie = Request.Cookies["ManagerShop_Cookies"] != null ? Request.Cookies["ManagerShop_Cookies"].Value : string.Empty;
                if (cookie != null && !string.IsNullOrEmpty(cookie))
                {
                    jwtCookie = JsonConvert.DeserializeObject<JwtCookie>(cookie);
                    var request_url = "/api/Media/SetActive";
                    var jsonData = JsonConvert.SerializeObject(lckMedia);
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