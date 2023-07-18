using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_API.Auth;
using QLBH_Services.UnitOfWork;

namespace QLBH_API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticController : ControllerBase
    {

        private readonly IUnitWork _unitWork;

        public StatisticController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }


        [HttpGet("ThongKeLoiNhuan")]
        public async Task<ActionResult> ThongKeLoiNhuan()
        {
            await Task.Yield();
            var lst = _unitWork.ThongKeLoiNhuanRepository.GetAll();
            return Ok(lst);
        }

        [HttpGet("ThongKeDoanhThu")]
        public async Task<ActionResult> ThongKeDoanhThu()
        {
            await Task.Yield();
            var lst = _unitWork.HoaDonCTRepository.GetTotalRec();
            return Ok(lst);
        }


        [HttpGet("ThongKeDonHang")]
        public async Task<ActionResult> ThongKeDonHang()
        {
            await Task.Yield();
            var lst = _unitWork.HoaDonRepository.GetTotalRec();
            return Ok(lst);
        }

        [HttpGet("ThongKeSanPham")]
        public async Task<ActionResult> ThongKeSanPham()
        {
            await Task.Yield();
            var lst = _unitWork.SanPhamRepository.GetTotalRec();
            return Ok(lst);
        }

        [HttpGet("ThongKeKhachHang")]
        public async Task<ActionResult> ThongKeKhachHang()
        {
            await Task.Yield();
            var lst = _unitWork.KhachHangRepository.GetTotalRec();
            return Ok(lst - 1);
        }
    }
}
