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
    public class BlogController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        public BlogController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        #region Blog

        [HttpGet("GetBlog")]
        [AllowAnonymous]
        public async Task<ActionResult> Blog_GetAll()
        {
            await Task.Yield();
            var lst = _unitWork.BlogRepository.GetAll();
            return Ok(lst);
        }

        [HttpPost("GetBlogPaging")]
        [AllowAnonymous]
        public async Task<ActionResult> Blog_GetAll_Paging(Page page)
        {
            await Task.Yield();
            var lst = _unitWork.BlogRepository.GetBlogPaging(page.page, page.pageSize);
            return Ok(lst);
        }

        [HttpPost("InsertBlog")]
        public async Task<ActionResult> InsertLoaiBlogType(Tintuc blg)
        {
            await Task.Yield();
            _unitWork.BlogRepository.Add(blg);
            var lst = _unitWork.Save();
            return Ok(lst);
        }


        [HttpDelete("DeleteBlog")]
        public async Task<ActionResult> DeleteBlog(string MaTinTuc)
        {
            await Task.Yield();
            var entity = _unitWork.BlogRepository.GetById(MaTinTuc);
            if (entity != null)
            {
                _unitWork.BlogRepository.Remove(entity);

            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpGet("GetBlogById")]
        public async Task<ActionResult> Blog_GetById(string MaTinTuc)
        {
            await Task.Yield();
            Tintuc lst = _unitWork.BlogRepository.GetById(MaTinTuc);
            return Ok(lst);
        }

        [HttpPut("UpdateBlog")]
        public async Task<ActionResult> UpdateBlog(Tintuc blg)
        {
            await Task.Yield();
            var entity = _unitWork.BlogRepository.GetById(blg.MaTinTuc);
            if (entity != null)
            {
                _unitWork.BlogRepository.Update(blg);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpGet("SearchBlog")]
        public async Task<ActionResult> SearchBlog(string MaTinTuc)
        {
            await Task.Yield();
            var lst = _unitWork.BlogRepository.SearchBlog(MaTinTuc);
            return Ok(lst);
        }

        [HttpGet("GetTotalRec")]
        [AllowAnonymous]
        public async Task<int> Blog_GetTotalRec()
        {
            await Task.Yield();
            int totalRow = _unitWork.BlogRepository.GetTotalRec();
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return totalRow;
        }

        [HttpGet("GetBlogSaleNews")]
        [AllowAnonymous]
        public async Task<Tintuc> GetBlogSaleNews()
        {
            await Task.Yield();
            Tintuc last = _unitWork.BlogRepository.GetBlogSaleNews();
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return last;
        }
        #endregion

        #region BlogType
        [HttpGet("GetBlogType")]
        [AllowAnonymous]
        public async Task<ActionResult> BlogType_GetAll()
        {
            await Task.Yield();
            var lst = _unitWork.BlogTypeRepository.GetAll();
            return Ok(lst);
        }

        [HttpPost("InsertBlogType")]
        public async Task<ActionResult> InsertLoaiBlogType(Loaitin blgtype)
        {
            await Task.Yield();
            _unitWork.BlogTypeRepository.Add(blgtype);
            var lst = _unitWork.Save();
            return Ok(lst);
        }


        [HttpDelete("DeleteBlogType")]
        public async Task<ActionResult> DeleteBlogType(string MaDm)
        {
            await Task.Yield();
            var entity = _unitWork.BlogTypeRepository.GetById(MaDm);
            if (entity != null)
            {
                _unitWork.BlogTypeRepository.Remove(entity);

            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpGet("GetBlogTypeById")]
        public async Task<ActionResult> BlogType_GetById(string MaDm)
        {
            await Task.Yield();
            Loaitin lst = _unitWork.BlogTypeRepository.GetById(MaDm);
            return Ok(lst);
        }

        [HttpPut("UpdateBlogType")]
        public async Task<ActionResult> UpdateBlogType(Loaitin blgtype)
        {
            await Task.Yield();
            var entity = _unitWork.BlogTypeRepository.GetById(blgtype.MaDm);
            if (entity != null)
            {
                _unitWork.BlogTypeRepository.Update(blgtype);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpGet("SearchBlogType")]
        public async Task<ActionResult> SearchBlogType(string TenDM)
        {
            await Task.Yield();
            var lst = _unitWork.BlogTypeRepository.Search(s => s.TenDm.Contains(TenDM));
            return Ok(lst);
        }

        #endregion
    }
}
