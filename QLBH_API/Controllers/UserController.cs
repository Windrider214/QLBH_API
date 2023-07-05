using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_API.Auth;
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
        [AllowAnonymous]
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

    }
}
