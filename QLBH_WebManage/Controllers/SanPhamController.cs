using Newtonsoft.Json;
using QLBH_WebManage.DTO;
using QLBH_WebManage.Helper;
using QLBH_WebManage.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using QLBH_WebManage.Helper;
using System.Runtime.Remoting.Messaging;

namespace QLBH_WebManage.Controllers
{
    public class SanPhamController : Controller
    {
        private static string url_api = ConfigurationManager.AppSettings["API_URL"] ?? "http://localhost:5050";
        private static string media_api = ConfigurationManager.AppSettings["MEDIA_URL"] ?? "http://localhost:63228";
        private static string accessToken = "";
        private static int totalRow = 0;

        public ActionResult PartialIndex(string TenSp, string token, int page, int pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(TenSp))
                {
                    List<SANPHAM> sp = new List<SANPHAM>();
                    var request_url = "/api/SanPham/GetSanPhamPaging";
                    Page pageData = new Page { page = page , pageSize = pageSize };
                    var jsonData = JsonConvert.SerializeObject(pageData);
                    var result = API_Interact.PullData(url_api, request_url, jsonData , "");
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        //sp = new JavaScriptSerializer().Deserialize<List<SANPHAM>>(result);
                        sp = JsonConvert.DeserializeObject<List<SANPHAM>>(result.Content);
                        return PartialView(sp);

                    }
                }
                else
                {
                    List<SANPHAM> sp = new List<SANPHAM>();
                    var request_url = "/api/SanPham/SearchSanPham";
                    var result = API_Interact.SearchDataByName(url_api, request_url, TenSp, "TenSp", token);
                    if (result.IsSuccessStatusCode)
                    {
                        accessToken = token;
                        sp = JsonConvert.DeserializeObject<List<SANPHAM>>(result.Content);
                        return PartialView(sp);

                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: SanPham
        public ActionResult Index()
        {
            try
            {
                var request_url = "/api/SanPham/GetTotalRec";
                var result = API_Interact.GetData(url_api, request_url, "");
                ViewBag.TotalRow = Int32.Parse(result);
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }
        
        public ActionResult GetThuongHieu(string token)
        {
            try
            {
               
                    List<THUONGHIEU> th = new List<THUONGHIEU>();
                    var request_url = "/api/ThuongHieu/GetThuongHieu";
                    var result = API_Interact.GetData(url_api, request_url, token);
                    if (!string.IsNullOrEmpty(result))
                    {
                        th = JsonConvert.DeserializeObject<List<THUONGHIEU>>(result);
                    }
                    return Json(th, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult GetLoaiSP(string token)
        {
            try
            {
                List<LOAISP> lsp = new List<LOAISP>();
                var request_url = "/api/SanPham/GetLoaiSP";
                var result = API_Interact.GetData(url_api, request_url, token);
                if (!string.IsNullOrEmpty(result))
                {
                    lsp = JsonConvert.DeserializeObject<List<LOAISP>>(result);
                }
                return Json(lsp, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Delete(string maSp)
        {
            var returnData = new ReturnData();
            try
            {
                if (maSp == "")
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Không tìm thấy mã sản phẩm !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var request_url = "/api/SanPham/DeleteSanPham";
                var result = API_Interact.DeleteData(url_api, request_url, maSp, "maSp", accessToken);

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

        public ActionResult Detail(string MaSP)
        {
            try
            {
                var sp = new SANPHAM();
                var request_url = "api/SanPham/GetSanPhamById";
                var result = API_Interact.GetDataById(url_api, request_url, MaSP, "maSp", accessToken);
                if (result.IsSuccessStatusCode)
                {
                    sp = JsonConvert.DeserializeObject<SANPHAM>(result.Content);
                    return View(sp);

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
        public ActionResult SanPhamInsert(SANPHAM sp)
        {
            var returnData = new ReturnData();
            try
            {
                if (sp.seoImage == null)
                {
                    sp.seoImage = "";
                }
                else
                {
                    Image img = new Image { imageName = sp.seoImage, alias = VNtoEnglish.UCS2Convert(sp.tenSp) + "SEO", folderName = "sanpham" + "\\" + "seoIMG" };
                    var saveImg_url = "/api/Image/SaveImage";
                    var imgData = JsonConvert.SerializeObject(img);
                    var imgString = API_Interact.SaveImage(media_api, saveImg_url, imgData);
                    if (!string.IsNullOrEmpty(imgString))
                    {
                        sp.seoImage = imgString.Replace("\"", "");
                    }
                }


                if (sp.image == null)
                {
                    sp.image = "";
                }
                else
                {
                    Image imgMulti = new Image { imageName = sp.image, alias = VNtoEnglish.UCS2Convert(sp.tenSp), folderName = "sanpham" };
                    var saveMultiImg_url = "/api/Image/SaveMultiImage";
                    var imgMultiData = JsonConvert.SerializeObject(imgMulti);
                    var imgMultiString = API_Interact.SaveImage(media_api, saveMultiImg_url, imgMultiData);
                    if (!string.IsNullOrEmpty(imgMultiString))
                    {
                        sp.image = imgMultiString.Replace("\"", "");
                    }
                }

                var request_url = "/api/SanPham/InsertSanPham";
                sp.maSp = Guid.NewGuid().ToString();
 
                var jsonData = JsonConvert.SerializeObject(sp);
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
        public ActionResult SanPhamUpdate(SANPHAM sp)
        {
            var returnData = new ReturnData();
            try
            {
                var sp_current = new SANPHAM();
                var request_url_current = "api/SanPham/GetSanPhamById";
                var result_current = API_Interact.GetDataById(url_api, request_url_current, sp.maSp, "maSp", accessToken);
                if (result_current.IsSuccessStatusCode)
                {
                    sp_current = JsonConvert.DeserializeObject<SANPHAM>(result_current.Content);
                }

                if (sp.seoImage == null)
                {
                    sp.seoImage = sp_current.seoImage;
                }
                else
                {
                    Image img = new Image { imageName = sp.seoImage, alias = VNtoEnglish.UCS2Convert(sp.tenSp) + "SEO", folderName = "sanpham" + "\\" + "seoIMG" };
                    var saveImg_url = "/api/Image/SaveImage";
                    var imgData = JsonConvert.SerializeObject(img);
                    var imgString = API_Interact.SaveImage(media_api, saveImg_url, imgData);
                    if (!string.IsNullOrEmpty(imgString))
                    {
                        sp.seoImage = imgString.Replace("\"", "");
                    }
                }


                if (sp.image == null)
                {
                    sp.image = sp_current.image;
                }
                else
                {
                    Image imgMulti = new Image { imageName = sp.image, alias = VNtoEnglish.UCS2Convert(sp.tenSp), folderName = "sanpham" };
                    var saveMultiImg_url = "/api/Image/SaveMultiImage";
                    var imgMultiData = JsonConvert.SerializeObject(imgMulti);
                    var imgMultiString = API_Interact.SaveImage(media_api, saveMultiImg_url, imgMultiData);
                    if (!string.IsNullOrEmpty(imgMultiString))
                    {
                        sp.image = sp_current.image + imgMultiString.Replace("\"", "");
                    }
                }

                var request_url = "/api/SanPham/UpdateSanPham";
                var jsonData = JsonConvert.SerializeObject(sp);
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DeleteSingleImg(SANPHAM sp, string delImage)
        {
            var returnData = new ReturnData();
            try
            {
                if(delImage == string.Empty)
                {
                    returnData.ResponseCode = -3;
                    returnData.Description = "Hãy chọn ảnh cần xóa";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }    

                string[] imageList = sp.image.Split(',');
                imageList = imageList.Where(s => s != delImage).ToArray();
                sp.image = String.Join(",", imageList);

                var request_url = "/api/SanPham/UpdateSanPham";
                var jsonData = JsonConvert.SerializeObject(sp);
                var result = API_Interact.UpdateData(url_api, request_url, jsonData, accessToken);

                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Xóa thành công";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Xóa thất bại !!!";
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
        public ActionResult btnAcceptUpdate_PosionImage(SANPHAM sp)
        {
            var returnData = new ReturnData();
            try
            {
                sp.image = sp.image + ",";
                var request_url = "/api/SanPham/UpdateSanPham";
                var jsonData = JsonConvert.SerializeObject(sp);
                var result = API_Interact.UpdateData(url_api, request_url, jsonData, accessToken);

                if (result.IsSuccessStatusCode)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Cập nhật thành công";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Cập nhật thất bại !!!";
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