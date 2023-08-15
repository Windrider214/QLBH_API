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
    public class PhanHoiController : ControllerBase
    {

        public readonly IEmailService _emailService;
        private readonly IUnitWork _unitWork;

        public PhanHoiController(IUnitWork unitWork, IEmailService emailService)
        {
            _unitWork = unitWork;
            _emailService = emailService;
        }


        [HttpPost("GetFeedbackPaging")]
        public async Task<ActionResult> GetFeedbackPaging(Page page)
        {
            await Task.Yield();
            List<Phanhoi> lst = _unitWork.PhanHoiRepository.GetListPaging(page.page, page.pageSize);
            return Ok(lst);
        }

        [HttpGet("GetTotalRec")]
        [AllowAnonymous]
        public async Task<int> GetTotalRec()
        {
            await Task.Yield();
            int totalRow = _unitWork.PhanHoiRepository.GetTotalRec();
            return totalRow;
        }

        [HttpGet("GetFeedbackByID")]
        [AllowAnonymous]
        public async Task<ActionResult> GetFeedbackByID(string MaPH)
        {
            await Task.Yield();
            Phanhoi lst = _unitWork.PhanHoiRepository.GetById(MaPH);
            return Ok(lst);
        }

        [HttpPost("InsertFeedback")]
        [AllowAnonymous]
        public async Task<ActionResult> InsertFeedback(Phanhoi ph)
        {
            await Task.Yield();
            _unitWork.PhanHoiRepository.Add(ph);
            var lst = _unitWork.Save();
            return Ok(lst);
        }


        [HttpDelete("DeleteFeedback")]
        public async Task<ActionResult> DeleteFeedback(string MaPH)
        {
            await Task.Yield();
            var entity = _unitWork.PhanHoiRepository.GetById(MaPH);
            if (entity != null)
            {
                _unitWork.PhanHoiRepository.Remove(entity);

            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpPut("UpdateFeedback")]
        public async Task<ActionResult> UpdateFeedback(Phanhoi ph)
        {
            await Task.Yield();
            var entity = _unitWork.PhanHoiRepository.GetById(ph.MaPh);
            if (entity != null)
            {
                _unitWork.PhanHoiRepository.Update(ph);
            }
            var lst = _unitWork.Save();
            return Ok(lst);
        }

        [HttpPost("GetListPagingByDate")]
        [AllowAnonymous]
        public async Task<ActionResult> GetListPagingByDate(OrderByDate order)
        {
            await Task.Yield();
            List<Phanhoi> lst = _unitWork.PhanHoiRepository.GetListPagingByDate(order.page, order.pageSize, order.StartDate, order.EndDate);
            return Ok(lst);
        }

        [HttpGet("SearchFeedback")]
        [AllowAnonymous]
        public async Task<ActionResult> SearchFeedback(string TieuDe)
        {
            await Task.Yield();
            List<Phanhoi> lst = _unitWork.PhanHoiRepository.SearchPH(TieuDe);
            return Ok(lst);
        }

        [HttpGet("GetFeedbackByCustomerID")]
        [AllowAnonymous]
        public async Task<ActionResult> GetFeedbackByCustomerID(string MaKH)
        {
            await Task.Yield();
            var lst = _unitWork.PhanHoiRepository.GetFeedbackByCustomerID(MaKH);
            return Ok(lst);
        }

        [HttpPost("GetListPagingByCusID")]
        [AllowAnonymous]
        public async Task<ActionResult> GetListPagingByCusID(CusFeedback fb)
        {
            await Task.Yield();
            List<Phanhoi> lst = _unitWork.PhanHoiRepository.GetListPagingByCusID(fb.page, fb.pageSize, fb.MaKH);
            return Ok(lst);
        }

        [HttpGet("GetCusTotalFeedback")]
        [AllowAnonymous]
        public async Task<int> GetCusTotalFeedback(string MaKH)
        {
            await Task.Yield();
            int totalRow = _unitWork.PhanHoiRepository.GetCusTotalFeedback(MaKH);
            return totalRow;
        }

        [HttpPost("SendEmailConfirmFeedback")]
        public async Task<IActionResult> SendEmailConfirmFeedback(ClientConfirmEmail confirmMessage)
        {
            await Task.Yield();

            string body = @"<div> '" + confirmMessage.deliverCode + "' </div>";
                var message = new Message(new string[] { confirmMessage.userEmail }, "Trả lời câu hỏi / yêu cầu tại ZAY SHOP '"+ confirmMessage.orderCode + "' ", body!);
                _emailService.SendEmail(message);
            
           
            return StatusCode(StatusCodes.Status200OK, new ReturnData { ResponseCode = 900, Description = "Gửi email xác nhận thành công" });
        }
    }
}
