using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_API.Auth;
using QLBH_API.Helper;
using QLBH_DataAccess.Models;
using QLBH_Services.UnitOfWork;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace QLBH_API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class SanPhamController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        public SanPhamController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        #region SanPham 
        [HttpGet("GetSanPham")]
        public async Task<ActionResult> SanPham_GetAll()
        {
            await Task.Yield();
            List<Sanpham> lst = _unitWork.SanPhamRepository.GetListSP();
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return Ok(lst);
        }

        [HttpPost("GetSanPhamPaging")]
        public async Task<ActionResult> SanPham_GetAll_Paging(Page page)
        {
            await Task.Yield();
            List<Sanpham> lst = _unitWork.SanPhamRepository.GetListPaging(page.page, page.pageSize);
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return Ok(lst);
        }

        [HttpPost("GetSanPhamActivePaging")]
        [AllowAnonymous]
        public async Task<ActionResult> GetSanPhamActivePaging(Page page)
        {
            await Task.Yield();
            List<Sanpham> lst = _unitWork.SanPhamRepository.GetListPagingActive(page.page, page.pageSize);
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return Ok(lst);
        }

        [HttpPost("GetSanPhamByTypePaging")]
        [AllowAnonymous]
        public async Task<ActionResult> GetSanPhamByTypePaging(ProducFilter proc)
        {
            await Task.Yield();
            List<Sanpham> lst = _unitWork.SanPhamRepository.GetListPagingByType(proc.page, proc.pageSize, proc.MaLoai);
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return Ok(lst);
        }


        [HttpGet("GetTotalRecByType")]
        [AllowAnonymous]
        public async Task<int> GetTotalRecByType(string maloai)
        {
            await Task.Yield();
            int totalRow = _unitWork.SanPhamRepository.GetTotalRecByType(maloai);
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return totalRow;
        }

        [HttpPost("GetSanPhamByBrandPaging")]
        [AllowAnonymous]
        public async Task<ActionResult> GetSanPhamByBrandPaging(ProducFilter proc)
        {
            await Task.Yield();
            List<Sanpham> lst = _unitWork.SanPhamRepository.GetListPagingByBrand(proc.page, proc.pageSize, proc.MaTH);
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return Ok(lst);
        }


        [HttpGet("GetTotalRecByBrand")]
        [AllowAnonymous]
        public async Task<int> GetTotalRecByBrand(string math)
        {
            await Task.Yield();
            int totalRow = _unitWork.SanPhamRepository.GetTotalRecByBrand(math);
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return totalRow;
        }

        [HttpGet("GetTop5")]
        [AllowAnonymous]
        public async Task<ActionResult> GetTop5()
        {
            await Task.Yield();
            List<Sanpham> lst = _unitWork.SanPhamRepository.GetTop5();
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return Ok(lst);
        }

        [HttpGet("GetTotalRec")]
        [AllowAnonymous]
        public async Task<int> SanPham_GetTotalRec()
        {
            await Task.Yield();
            int totalRow = _unitWork.SanPhamRepository.GetTotalRec();
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return totalRow;
        }

        [HttpPost("InsertSanPham")]
        public async Task<ActionResult> InsertSanPham(Sanpham sp)
        {
            await Task.Yield();
            _unitWork.SanPhamRepository.Add(sp);
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpDelete("DeleteSanPham")]
        public async Task<ActionResult> DeleteSanPham(string maSp)
        {
            await Task.Yield();
            var entity = _unitWork.SanPhamRepository.GetById(maSp);
            if (entity != null)
            {
                _unitWork.SanPhamRepository.Remove(entity);

            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }


        [HttpGet("GetSanPhamById")]
        public async Task<ActionResult> SanPham_GetById(string MaSP)
        {
            await Task.Yield();
            Sanpham lst = _unitWork.SanPhamRepository.GetById(MaSP);
            return Ok(lst);
        }


        [HttpGet("GetSanPhamByIdClient")]
        [AllowAnonymous]
        public async Task<ActionResult> GetSanPhamByIdClient(string MaSP)
        {
            await Task.Yield();
            Sanpham lst = _unitWork.SanPhamRepository.GetByIdClient(MaSP);
            return Ok(lst);
        }

        [HttpPut("UpdateSanPham")]
        public async Task<ActionResult> UpdateSanPham(Sanpham sp)
        {
            await Task.Yield();
            var entity = _unitWork.SanPhamRepository.GetById(sp.MaSp);
            if (entity != null)
            {
                _unitWork.SanPhamRepository.Update(sp);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpGet("SuggestSanPham")]
        [AllowAnonymous]
        public async Task<ActionResult> SuggestSanPham(string TenSP)
        {
            await Task.Yield();
            var lst = _unitWork.SanPhamRepository.SearchSuggest(TenSP);
            return Ok(lst);
        }

        [HttpGet("SearchSanPham")]
        [AllowAnonymous]
        public async Task<ActionResult> SearchSanPham(string TenSP)
        {
            await Task.Yield();
            var lst = _unitWork.SanPhamRepository.SearchSP(TenSP);
            return Ok(lst);
        }

        [HttpGet("CapNhatLuotXem")]
        [AllowAnonymous]
        public async Task<ActionResult> CapNhatLuotXem(string MaSP)
        {
            await Task.Yield();
            var entity = _unitWork.SanPhamRepository.GetById(MaSP);
            if (entity != null)
            {
                entity.SoLanXem = entity.SoLanXem + 1;
                _unitWork.SanPhamRepository.Update(entity);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }
        #endregion

        #region LoaiSanPham
        [HttpGet("GetLoaiSP")]
        [AllowAnonymous]
        public async Task<ActionResult> LoaiSanPham_GetAll()
        {
            await Task.Yield();
            var lst = _unitWork.LoaiSPRepository.GetAll();
            return Ok(lst);
        }

        [HttpPost("InsertLoaiSP")]
        public async Task<ActionResult> InsertLoaiSanPham(Loaisp lsp)
        {
            await Task.Yield();
            _unitWork.LoaiSPRepository.Add(lsp);
            var lst = _unitWork.Save(); 
            return Ok(lst);
        }


        [HttpDelete("DeleteLoaiSP")]
        public async Task<ActionResult> DeleteLoaiSanPham(string MaLoai)
        {
            await Task.Yield();
            var entity = _unitWork.LoaiSPRepository.GetById(MaLoai);
            if (entity != null)
            {
               _unitWork.LoaiSPRepository.Remove(entity);

            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpGet("GetLoaiSPById")]
        [AllowAnonymous]
        public async Task<ActionResult> LoaiSanPham_GetById(string MaLoai)
        {
            await Task.Yield();
            Loaisp lst = _unitWork.LoaiSPRepository.GetById(MaLoai);
            return Ok(lst);
        }

        [HttpPut("UpdateLoaiSP")]
        public async Task<ActionResult> UpdateLoaiSP(Loaisp lsp)
        {
            await Task.Yield();
            var entity = _unitWork.LoaiSPRepository.GetById(lsp.MaLoai);
            if(entity != null)
            {
                _unitWork.LoaiSPRepository.Update(lsp);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }


        [HttpGet("SearchLoaiSP")]
        public async Task<ActionResult> SearchLoaiSP(string TenLoaiSP)
        {
            await Task.Yield();
            var lst = _unitWork.LoaiSPRepository.Search(s => s.TenLoaiSp.Contains(TenLoaiSP));
            return Ok(lst);
        }

        #endregion

    }
}
