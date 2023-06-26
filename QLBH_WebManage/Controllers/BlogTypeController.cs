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
    public class BlogTypeController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:63228";
        private static string accessToken = "";

        public ActionResult PartialIndex(string TenDm, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(TenDm))
                {
                    List<BLOGTYPE> blgtype = new List<BLOGTYPE>();
                    var request_url = "/api/Blog/GetBlogType";
                    var result = API_Interact.GetData(url_api, request_url, token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        accessToken = token;
                        blgtype = JsonConvert.DeserializeObject<List<BLOGTYPE>>(result);
                        return PartialView(blgtype);

                    }
                }
                else
                {
                    List<BLOGTYPE> blgtype = new List<BLOGTYPE>();
                    var request_url = "/api/Blog/SearchBlogType";
                    var result = API_Interact.SearchDataByName(url_api, request_url, TenDm, "TenDm", token);
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        blgtype = JsonConvert.DeserializeObject<List<BLOGTYPE>>(result.Content);
                        return PartialView(blgtype);

                    }
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: BlogType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Delete(string MaDM)
        {
            var returnData = new ReturnData();
            try
            {
                if (MaDM == "")
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Không tìm thấy mã danh mục tin !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var request_url = "/api/Blog/DeleteBlogType";
                var result = API_Interact.DeleteData(url_api, request_url, MaDM, "MaDM", accessToken);

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

        public ActionResult Detail(string MaDM)
        {
            try
            {
                var blgtype = new BLOGTYPE();
                var request_url = "api/Blog/GetBlogTypeById";
                var result = API_Interact.GetDataById(url_api, request_url, MaDM, "MaDM", accessToken);
                if (result.IsSuccessStatusCode)
                {
                    blgtype = JsonConvert.DeserializeObject<BLOGTYPE>(result.Content);
                    return View(blgtype);

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
        public ActionResult BlogTypeInsert(BLOGTYPE blgtype)
        {
            var returnData = new ReturnData();
            try
            {
              
                var request_url = "/api/Blog/InsertBlogType";
                blgtype.maDm = Guid.NewGuid().ToString();
                var jsonData = JsonConvert.SerializeObject(blgtype);
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
        public ActionResult BlogTypeUpdate(BLOGTYPE blgtype)
        {
            var returnData = new ReturnData();
            try
            {

                var request_url = "/api/Blog/UpdateBlogType";

                var jsonData = JsonConvert.SerializeObject(blgtype);
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