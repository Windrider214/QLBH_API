using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_API.Auth;
using QLBH_API.Helper;
using QLBH_DataAccess.Models;
using QLBH_Services.UnitOfWork;

namespace QLBH_API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        public MediaController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        [HttpGet("GetMedia")]
        public async Task<ActionResult> Media_GetAll()
        {
            await Task.Yield();
            var lst = _unitWork.MediaRepository.GetListMedia();
            return Ok(lst);
        }

        [HttpPost("InsertMedia")]
        public async Task<ActionResult> InsertMedia(Medium med)
        {
            await Task.Yield();
            _unitWork.MediaRepository.Add(med);
            var lst = _unitWork.Save();
            return Ok(lst);
        }


        [HttpDelete("DeleteMedia")]
        public async Task<ActionResult> DeleteMedia(string MediaID)
        {
            await Task.Yield();
            var entity = _unitWork.MediaRepository.GetById(MediaID);
            if (entity != null)
            {
                _unitWork.MediaRepository.Remove(entity);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpGet("GetMediaById")]
        public async Task<ActionResult> Media_GetById(string MediaID)
        {
            await Task.Yield();
            var lst = _unitWork.MediaRepository.GetById(MediaID);
            return Ok(lst);
        }

        [HttpPut("UpdateMedia")]
        public async Task<ActionResult> UpdateMedia(Medium med)
        {
            await Task.Yield();
            var entity = _unitWork.MediaRepository.GetById(med.MediaId.ToString());
            if (entity != null)
            {
                _unitWork.MediaRepository.Update(med);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpPost("SetActive")]
        public async Task<ActionResult> SetActive(LockMedia lckMed)
        {
            await Task.Yield();
            var entity = _unitWork.MediaRepository.GetById(lckMed.MediaId);
            if (entity != null)
            {
               if(lckMed.isActive == true)
                {
                    entity.IsActive = true;
                    _unitWork.MediaRepository.Update(entity);
                }
                else
                {
                    entity.IsActive = false;
                    _unitWork.MediaRepository.Update(entity);
                }
            }
            var lst = _unitWork.Save();
            return Ok();
        }

        [HttpGet("GetMediaType")]
        public async Task<ActionResult> MediaType_GetAll()
        {
            await Task.Yield();
            var lst = _unitWork.MediaTypeRepository.GetAll();
            return Ok(lst);
        }

        [HttpGet("GetBanner")]
        [AllowAnonymous]
        public async Task<ActionResult> Banner_GetAll()
        {
            await Task.Yield();
            var lst = _unitWork.MediaRepository.GetBanner();
            return Ok(lst);
        }

        [HttpGet("GetLogo")]
        [AllowAnonymous]
        public async Task<ActionResult> GetLogo()
        {
            await Task.Yield();
            var lst = _unitWork.MediaRepository.GetLogo();
            return Ok(lst);
        }

    }
}
