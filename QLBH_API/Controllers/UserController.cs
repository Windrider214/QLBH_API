using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_API.Auth;
using QLBH_Services.UnitOfWork;

namespace QLBH_API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        public UserController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
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
