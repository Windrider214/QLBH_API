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
    public class PhanHoiController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:63228";
        private static string accessToken = "";


        // GET: PhanHoi
        public ActionResult Index()
        {
            try
            {
                var request_url = "/api/PhanHoi/GetTotalRec";
                var result = API_Interact.GetData(url_api, request_url, "");
                ViewBag.TotalRow = Int32.Parse(result);
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult PartialIndex(string tieuDe, string token, int page, int pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(tieuDe))
                {
                    List<PHANHOI> ph = new List<PHANHOI>();
                    var request_url = "/api/PhanHoi/GetFeedbackPaging";
                    Page pageData = new Page { page = page, pageSize = pageSize };
                    var jsonData = JsonConvert.SerializeObject(pageData);
                    var result = API_Interact.PullData(url_api, request_url, jsonData, token);
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        //sp = new JavaScriptSerializer().Deserialize<List<SANPHAM>>(result);
                        ph = JsonConvert.DeserializeObject<List<PHANHOI>>(result.Content);
                        return PartialView(ph);
                    }
                }
                else
                {
                    List<PHANHOI> ph = new List<PHANHOI>();
                    var request_url = "/api/PhanHoi/SearchFeedback";
                    var result = API_Interact.SearchDataByName(url_api, request_url, tieuDe, "TieuDe", token);
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        ph = JsonConvert.DeserializeObject<List<PHANHOI>>(result.Content);
                        return PartialView(ph);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult Detail(string MaPH)
        {
            try
            {
                var ph = new PHANHOI();
                var request_url = "api/PhanHoi/GetFeedbackByID";
                var result = API_Interact.GetDataById(url_api, request_url, MaPH, "MaPH", accessToken);
                if (result.IsSuccessStatusCode)
                {
                    ph = JsonConvert.DeserializeObject<PHANHOI>(result.Content);
                    return View(ph);

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
        public ActionResult FeedbackUpdate(PHANHOI ph)
        {
            var returnData = new ReturnData();
            try
            {
                var request_url = "/api/PhanHoi/UpdateFeedback";

                var jsonData = JsonConvert.SerializeObject(ph);
                var result = API_Interact.UpdateData(url_api, request_url, jsonData, accessToken);

                if (result.IsSuccessStatusCode)
                {
                    // Send email confirm
                    ClientConfirmEmail email = new ClientConfirmEmail();
                    email.userEmail = ph.email;
                    email.isCreatedOrder = true;
                    email.orderCode = ph.tieuDe;
                    email.deliverCode = ph.phanHoi1;
                    SendEmailConfirmFeedback(email);
                    returnData.ResponseCode = 1;
                    returnData.Description = "Trả lời phản hồi thành công !!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Trả lời phản hồi thất bại !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Tạo email
        /// </summary>
        /// <param name="email"></param>
        public void SendEmailConfirmFeedback(ClientConfirmEmail email)
        {
            var returnData = new ReturnData();

            try
            {
                var request_url = "/api/PhanHoi/SendEmailConfirmFeedback";
                var jsonData = JsonConvert.SerializeObject(email);
                var result = API_Interact.InsertData(url_api, request_url, jsonData, accessToken);
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Status = "OK";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Delete(string MaPH)
        {
            var returnData = new ReturnData();
            try
            {
                if (MaPH == "")
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Không tìm thấy mã ph !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var request_url = "/api/PhanHoi/DeleteFeedback";
                var result = API_Interact.DeleteData(url_api, request_url, MaPH, "MaPH", accessToken);

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
    }
}