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
    public class UserController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        public UserController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        [HttpPost("AuthCheck")]
        [Authorize]
        public async Task<ActionResult> AuthCheck()
        {
            await Task.Yield();
            return Ok();
        }

        [HttpGet("GetUserById")]
        [Authorize]
        public async Task<ActionResult> GetUserById(string userID)
        {
            await Task.Yield();
            var lst = _unitWork.UserRepository.GetUserByID(userID);
            return Ok(lst);
        }

        [HttpGet("GetUser")]
        [AllowAnonymous]
        public async Task<ActionResult> User_GetAll()
        {
            await Task.Yield();
            var lst = _unitWork.UserRepository.GetAll();
            return Ok(lst);
        }


        [HttpPost("GetAllUsersPaging")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllUsersPaging(Page page)
        {
            await Task.Yield();
            var lst = _unitWork.UserRepository.GetAllUsersPaging(page.page, page.pageSize);
            return Ok(lst);
        }

        [HttpGet("SearchUser")]
        [AllowAnonymous]
        public async Task<ActionResult> SearchUser(string UserName)
        {
            await Task.Yield();
            var lst = _unitWork.UserRepository.SearchUser(UserName);
            return Ok(lst);
        }

        [HttpGet("GetTotalRec")]
        [AllowAnonymous]
        public async Task<int> User_GetTotalRec()
        {
            await Task.Yield();
            int totalRow = _unitWork.UserRepository.GetTotalRec();
            //var lst = _unitWork.SanPhamRepository.GetAll();
            return totalRow;
        }

        #region ROLE
        [HttpGet("GetRole")]
        public async Task<ActionResult> GetRole()
        {
            await Task.Yield();
            List<AspNetRole> lst = _unitWork.RoleRepository.GetAll();
            return Ok(lst);
        }

        #endregion
    }
}