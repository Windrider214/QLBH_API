using Microsoft.AspNetCore.Mvc;
using MEDIA_SERVER.Models;

namespace MEDIA_SERVER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ImageController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("SaveImage")]
        public string SaveImage(Image imgData)
        {
            string imgNameReturn = string.Empty;
            try
            {
                return SaveImagePath(imgData.imageName, imgData.alias, imgData.folderName);
            }
            catch (Exception ex)
            {
                throw;

            }
            return imgNameReturn;
        }

        [HttpPost("SaveMultiImage")]
        public string SaveMultiImage(Image imgData)
        {
            string imgNameReturn = string.Empty;
            try
            {
                var multiImg = string.Empty;

                if (imgData.imageName != "")
                {
                    if (imgData.imageName.Split('_').Length > 0) // Có nhiều hơn 1 ảnh
                    {
                        for (int i = 0; i < imgData.imageName.Split('_').Length; i++)
                        {
                            var image_base64 = imgData.imageName.Split('_')[i];
                            multiImg += SaveImagePath(image_base64, imgData.alias, imgData.folderName) + ",";
                        }
                    }
                }
                return multiImg;

            }
            catch (Exception ex)
            {
                throw;

            }
        }


        private string SaveImagePath(string imageName, string alias, string folderName)
        {
            string imgNameReturn = string.Empty;
            try
            {
                string fullPath = _config.GetSection("URL:Image_Path").Value + folderName + "\\";
                string urlPatch = _config.GetSection("URL:Url_Path").Value;
                byte[] imageByte = Convert.FromBase64String(imageName);
                MemoryStream ms = new MemoryStream(imageByte, 0, imageByte.Length);
                ms.Write(imageByte, 0, imageByte.Length);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms, true);
                var filename = string.Format("{0}{1}", alias + "_" + DateTime.Now.Ticks, ".jpg");
                string filePath = string.Format("{0}{1}", (fullPath), filename);
                img.Save(filePath);

                return filename;
            }
            catch (Exception ex)
            {
                return string.Empty;

            }
            return imgNameReturn;
        }
    }
}
