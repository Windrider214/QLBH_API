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
    public class BlogController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:5127";
        private static string accessToken = "";

        public ActionResult PartialIndex(string MaTinTuc, string token, int page, int pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(MaTinTuc))
                {
                    List<BLOG> blg = new List<BLOG>();
                    var request_url = "/api/Blog/GetBlogPaging";
                    Page pageData = new Page { page = page, pageSize = pageSize };
                    var jsonData = JsonConvert.SerializeObject(pageData);
                    var result = API_Interact.PullData(url_api, request_url, jsonData, token);
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        blg = JsonConvert.DeserializeObject<List<BLOG>>(result.Content);
                        return PartialView(blg);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(404, "Hệ thống không xác thực, xin mời đăng nhập lại !!!");
                    }
                }
                else
                {
                    List<BLOG> blg = new List<BLOG>();
                    var request_url = "/api/Blog/SearchBlog";
                    var result = API_Interact.SearchDataByName(url_api, request_url, MaTinTuc, "MaTinTuc", token);
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        blg = JsonConvert.DeserializeObject<List<BLOG>>(result.Content);
                        return PartialView(blg);

                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        // GET: Blog
        public ActionResult Index()
        {
            try
            {
                var request_url = "/api/Blog/GetTotalRec";
                var result = API_Interact.GetData(url_api, request_url, "");
                if(!string.IsNullOrEmpty(result))
                {
                    ViewBag.TotalRow = Int32.Parse(result);
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult GetBlogType(string token)
        {
            try
            {
                List<BLOGTYPE> blgtype = new List<BLOGTYPE>();
                var request_url = "/api/Blog/GetBlogType";
                var result = API_Interact.GetData(url_api, request_url, token);
                if (!string.IsNullOrEmpty(result))
                {
                    accessToken = token;
                    blgtype = JsonConvert.DeserializeObject<List<BLOGTYPE>>(result);
                }
                return Json(blgtype, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult GetUser(string token)
        {
            try
            {
                List<USER> user = new List<USER>();
                var request_url = "/api/User/GetUser";
                var result = API_Interact.GetData(url_api, request_url, token);
                if (!string.IsNullOrEmpty(result))
                {
                    accessToken = token;
                    user = JsonConvert.DeserializeObject<List<USER>>(result);
                }
                return Json(user, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BlogInsert(BLOG blg)
        {
            var returnData = new ReturnData();
            try
            {
                if (blg.anhTt == null)
                {
                    blg.anhTt = "";
                }
                else
                {
                    Image img = new Image { imageName = blg.anhTt, alias = "blogs", folderName = "blogs" };
                    var saveImg_url = "/api/Image/SaveImage";
                    var imgData = JsonConvert.SerializeObject(img);
                    var imgString = API_Interact.SaveImage(media_api, saveImg_url, imgData);
                    if (!string.IsNullOrEmpty(imgString))
                    {
                        blg.anhTt = imgString.Replace("\"", "");
                    }
                }

                blg.maTinTuc = Guid.NewGuid().ToString();
                var request_url = "/api/Blog/InsertBlog";
                var jsonData = JsonConvert.SerializeObject(blg);
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

        public ActionResult Delete(string MaTinTuc)
        {
            var returnData = new ReturnData();
            try
            {
                if (MaTinTuc == "")
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Không tìm thấy bài viết !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var request_url = "/api/Blog/DeleteBlog";
                var result = API_Interact.DeleteData(url_api, request_url, MaTinTuc, "MaTinTuc", accessToken);

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

        public ActionResult Detail(string MaTinTuc)
        {
            try
            {
                var blg = new BLOG();
                var request_url = "api/Blog/GetBlogById";
                var result = API_Interact.GetDataById(url_api, request_url, MaTinTuc, "MaTinTuc", accessToken);
                if (result.IsSuccessStatusCode)
                {
                    blg = JsonConvert.DeserializeObject<BLOG>(result.Content);
                    return View(blg);

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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BlogUpdate(BLOG blg)
        {
            var returnData = new ReturnData();
            try
            {
                if (CheckBase64.IsBase64String(blg.anhTt))
                {
                    Image img = new Image { imageName = blg.anhTt, alias = "blog", folderName = "blogs" };
                    var saveImg_url = "/api/Image/SaveImage";
                    var imgData = JsonConvert.SerializeObject(img);
                    var imgString = API_Interact.SaveImage(media_api, saveImg_url, imgData);
                    if (!string.IsNullOrEmpty(imgString))
                    {
                        blg.anhTt = imgString.Replace("\"", "");
                    }
                }

                var request_url = "/api/Blog/UpdateBlog";
                var jsonData = JsonConvert.SerializeObject(blg);
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