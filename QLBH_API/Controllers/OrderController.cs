using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_API.Auth;
using QLBH_API.Email;
using QLBH_API.Helper;
using QLBH_DataAccess.Models;
using QLBH_Services.UnitOfWork;

namespace QLBH_API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
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

        [HttpPost("GetOrdersPaging")]
        public async Task<ActionResult> GetOrdersPaging(Page page)
        {
            await Task.Yield();
            List<Hoadon> lst = _unitWork.HoaDonRepository.GetListPaging(page.page, page.pageSize);
            return Ok(lst);
        }

        [HttpGet("GetTotalRec")]
        [AllowAnonymous]
        public async Task<int> GetTotalRec()
        {
            await Task.Yield();
            int totalRow = _unitWork.HoaDonRepository.GetTotalRec();
            return totalRow;
        }

        [HttpGet("GetOrdersByID")]
        [AllowAnonymous]
        public async Task<ActionResult> GetOrdersByID(string mahd)
        {
            await Task.Yield();
            Hoadon lst = _unitWork.HoaDonRepository.GetOrderByID(mahd);
            return Ok(lst);
        }

        [HttpDelete("DeleteOrder")]
        public async Task<ActionResult> DeleteOrder(string maHd)
        {
            await Task.Yield();
            var entity = _unitWork.HoaDonRepository.GetById(maHd);
            if (entity != null)
            {
                _unitWork.HoaDonRepository.Remove(entity);

            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpPost("GetListPagingByDate")]
        [AllowAnonymous]
        public async Task<ActionResult> GetListPagingByDate(OrderByDate order)
        {
            await Task.Yield();
            List<Hoadon> lst = _unitWork.HoaDonRepository.GetListPagingByDate(order.page, order.pageSize, order.StartDate, order.EndDate); 
            return Ok(lst);
        }

        [HttpPost("GetListByDate")]
        [AllowAnonymous]
        public async Task<ActionResult> GetListByDate(DateRange dr)
        {
            await Task.Yield();
            List<Hoadon> lst = _unitWork.HoaDonRepository.GetListByDate(dr.Start, dr.End);
            return Ok(lst);
        }

        [HttpGet("SearchOrder")]
        public async Task<ActionResult> SearchOrder(string maHd)
        {
            await Task.Yield();
            List<Hoadon> lst = _unitWork.HoaDonRepository.SearchOrder(maHd);
            return Ok(lst);
        }

        [HttpPost("InsertOrders")]
        [AllowAnonymous]
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

        [HttpPut("UpdateOrder")]
        public async Task<ActionResult> UpdateOrder(Hoadon hd)
        {
            await Task.Yield();
            var entity = _unitWork.HoaDonRepository.GetById(hd.MaHd);
            if (entity != null)
            {
                _unitWork.HoaDonRepository.Update(hd);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpGet("GetListOrderProduct")]
        [AllowAnonymous]
        public async Task<ActionResult> GetListOrderProduct(string maHd)
        {
            await Task.Yield();
            List<Hoadonct> lst = _unitWork.HoaDonCTRepository.GetListOrderProcduct(maHd);
            return Ok(lst);
        }

        [HttpGet("GetOrdersByCustomerID")]
        [AllowAnonymous]
        public async Task<ActionResult> GetOrdersByCustomerID(string makh)
        {
            await Task.Yield();
            var lst = _unitWork.HoaDonRepository.GetListByCustomerID(makh);
            return Ok(lst);
        }


        [HttpPost("SendEmailConfirmOrder")]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmailConfirmOrder(ClientConfirmEmail confirmMessage)
        {
            await Task.Yield();
            if(confirmMessage.isCreatedOrder == true)
            {
                string body = @" <div>Đơn hàng <strong>'" + confirmMessage.orderCode + "'</strong> đã được đặt thàng công. </div>" + "</br>" +
                    " <div>Mã vận đơn của đơn hàng là <strong>'" + confirmMessage.deliverCode + "'</strong> </div>" + "</br>" +
               " <div>Quý khách có thể tra cứu mã vận đơn tại https://tracking.ghn.dev/ <div>" + "</br>" +
               " <div><i>Cảm ơn đã quý khách đã ủng hộ !!!</i></div> ";
                var message = new Message(new string[] { confirmMessage.userEmail }, "Thông báo đặt hàng thành công", body!);
                _emailService.SendEmail(message);
            }
            else
            {
                string body = @" <div>Có lỗi xảy ra trong quá trình tạo mã vận đơn </div>" + "</br>" +
              " <div>Vui lòng liên hệ với chúng tôi để nhận được mã vận đơn của quý khách !!! <div>" + "</br>" +
               " <div>Mã đơn hàng của quý khách là <strong>'"+ confirmMessage.orderCode +"'</strong> <div>" + "</br>" +
              " <div><i>Cảm ơn đã quý khách đã ủng hộ !!!</i></div> ";
                var message = new Message(new string[] { confirmMessage.userEmail }, "Thông báo đặt hàng thành công", body!);
                _emailService.SendEmail(message);
            }    
            return StatusCode(StatusCodes.Status200OK, new ReturnData { ResponseCode = 900, Description = "Gửi email xác nhận thành công" });
        }
    }
}
