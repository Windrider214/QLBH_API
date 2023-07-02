using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_API.Auth;
using QLBH_Services.UnitOfWork;

namespace QLBH_API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class KhachHangController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        public KhachHangController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        [HttpGet("GetKhachHang")]
        [AllowAnonymous]
        public async Task<ActionResult> KhachHang_GetAll()
        {
            await Task.Yield();
            var lst = _unitWork.KhachHangRepository.GetAll();
            return Ok(lst);
        }

        [HttpGet("GetKhachHangByUserID")]
        [AllowAnonymous]
        public async Task<ActionResult> GetKhachHangByUserID(string userID)
        {
            await Task.Yield();
            var lst = _unitWork.KhachHangRepository.GetByUserID(userID);
            return Ok(lst);
        }
    }
}
