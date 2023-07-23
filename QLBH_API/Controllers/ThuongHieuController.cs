using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using QLBH_API.Auth;
using QLBH_DataAccess.Models;
using QLBH_Services.UnitOfWork;
using System;

namespace QLBH_API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class ThuongHieuController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        public ThuongHieuController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        [HttpGet("GetThuongHieu")]
        [AllowAnonymous]
        public async Task<ActionResult> ThuongHieu_GetAll()
        {
            await Task.Yield();
            var lst = _unitWork.thuongHieuRepository.GetAll();
            return Ok(lst);
        }

        [HttpPost("InsertThuongHieu")]
        public async Task<ActionResult> InserthuongHieu(Thuonghieu th)
        {
            await Task.Yield();
            _unitWork.thuongHieuRepository.Add(th);
            var lst = _unitWork.Save();
            return Ok(lst);
        }


        [HttpDelete("DeleteThuongHieu")]
        public async Task<ActionResult> DeleteThuongHieu(string MaTh)
        {
            await Task.Yield();
            var entity = _unitWork.thuongHieuRepository.GetById(MaTh);
            if(entity != null)
            {
                _unitWork.thuongHieuRepository.Remove(entity);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpGet("GetThuongHieuById")]
        [AllowAnonymous]
        public async Task<ActionResult> ThuongHieu_GetById(string MaTh)
        {
            await Task.Yield();
            var lst = _unitWork.thuongHieuRepository.GetById(MaTh);
            return Ok(lst);
        }

        [HttpPut("UpdateThuongHieu")]
        public async Task<ActionResult> UpdateThuongHieu(Thuonghieu th)
        {
            await Task.Yield();
            var entity = _unitWork.thuongHieuRepository.GetById(th.MaTh.ToString());
            if (entity != null)
            {
                _unitWork.thuongHieuRepository.Update(th);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }


        [HttpGet("SearchThuongHieu")]
        public async Task<ActionResult> SearchThuongHieu(string TenTh)
        {
            await Task.Yield();
            var lst = _unitWork.thuongHieuRepository.Search(s => s.TenTh.Contains(TenTh.Trim()));
            return Ok(lst);
        }
    }
}
