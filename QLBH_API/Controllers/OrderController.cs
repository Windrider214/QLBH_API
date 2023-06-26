using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_API.Auth;
using QLBH_API.Email;
using QLBH_API.Helper;
using QLBH_DataAccess.Models;
using QLBH_Services.UnitOfWork;

namespace QLBH_API.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        public readonly IEmailService _emailService;
        private readonly IUnitWork _unitWork;

        public OrderController(IUnitWork unitWork, IEmailService emailService)
        {
            _unitWork = unitWork;
            _emailService = emailService;
        }

        [HttpGet("GetOrders")]
        [AllowAnonymous]
        public async Task<ActionResult> Orders()
        {
            await Task.Yield();
            var lst = _unitWork.HoaDonRepository.GetAll();
            return Ok(lst);
        }

        [HttpPost("InsertOrders")]
        public async Task<ActionResult> InsertOrders(OrderEditModel hd)
        {
            await Task.Yield();
            if(hd == null)
            {
                return BadRequest();
            }
            _unitWork.HoaDonRepository.Add(hd);

            if(hd.OrderDetail != null && hd.OrderDetail.Count > 0)
            {
                foreach (var item in hd.OrderDetail)
                {
                    _unitWork.HoaDonCTRepository.Add(item);
                }
            }
            else
            {
                return BadRequest();
            }

            var lst = _unitWork.Save();
            return Ok(lst);
        }


        [HttpPost("SendEmailConfirmOrder")]
        public async Task<IActionResult> SendEmailConfirmOrder(ClientConfirmEmail confirmMessage)
        {
            await Task.Yield();
            string body = @" <div>Đơn hàng <strong>'" + confirmMessage.deliverCode + "'</strong> đã được đặt thàng công. </div>" + "</br>" +
                           " <div>Quý khách có thể tra cứu đơn hàng <strong>'" + confirmMessage.deliverCode + "'</strong> tại https://tracking.ghn.dev/ <div>" + "</br>" +
                           " <div><i>Cảm ơn đã quý khách đã ủng hộ !!!</i></div> ";
            var message = new Message(new string[] { confirmMessage.userEmail }, "Thông báo đặt hàng thành công", body!);
            _emailService.SendEmail(message);
            return StatusCode(StatusCodes.Status200OK, new ReturnData { ResponseCode = 900, Description = "Gửi email xác nhận thành công" });


        }
    }
}
