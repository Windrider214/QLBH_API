using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_API.Auth;
using QLBH_API.Helper;
using QLBH_DataAccess.Models;
using QLBH_Services.UnitOfWork;

namespace QLBH_API.Controllers
{
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
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> KhachHang_GetAll()
        {
            await Task.Yield();
            var lst = _unitWork.KhachHangRepository.GetAll();
            return Ok(lst);
        }

        [HttpPost("GetAllKhPaging")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> GetAllKhPaging(Page page)
        {
            await Task.Yield();
            var lst = _unitWork.KhachHangRepository.GetListPaging(page.page, page.pageSize);
            return Ok(lst);
        }

        [HttpGet("SearchKH")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> SearchKH(string TenKH)
        {
            await Task.Yield();
            var lst = _unitWork.KhachHangRepository.SearchKH(TenKH);
            return Ok(lst);
        }

        [HttpGet("GetKhachHangByUserID")]
        [Authorize]
        public async Task<ActionResult> GetKhachHangByUserID(string userID)
        {
            await Task.Yield();
            var lst = _unitWork.KhachHangRepository.GetByUserID(userID);
            return Ok(lst);
        }

        [HttpPost("InsertKhachHang")]
        [AllowAnonymous]
        public async Task<ActionResult> InsertKhachHang(Khachhang kh)
        {
            await Task.Yield();
            _unitWork.KhachHangRepository.Add(kh);
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpPut("UpdateKhachHang")]
        [Authorize]
        public async Task<ActionResult> UpdateKhachHang(Khachhang kh)
        {
            await Task.Yield();
            var entity = _unitWork.KhachHangRepository.GetById(kh.MaKh.ToString());
            if (entity != null)
            {
                _unitWork.KhachHangRepository.Update(kh);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpGet("GetTotalRec")]
        [AllowAnonymous]
        public async Task<int> KhachHang_GetTotalRec()
        {
            await Task.Yield();
            int totalRow = _unitWork.KhachHangRepository.GetTotalRec();
            return totalRow;
        }
    }
}
