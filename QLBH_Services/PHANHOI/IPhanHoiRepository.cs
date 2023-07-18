using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.PHANHOI
{
    public interface IPhanHoiRepository : IGenericRepository<Phanhoi>
    {
        List<Phanhoi> SearchPH(string tieuDe);
        List<Phanhoi> GetListPaging(int page, int pageSize);

        List<Phanhoi> GetListPagingByDate(int page, int pageSize, DateTime startDate, DateTime endDate);
        List<Phanhoi> GetFeedbackByCustomerID(string MaKH);

        List<Phanhoi> GetListPagingByCusID(int page, int pageSize, string MaKH);
        int GetCusTotalFeedback(string MaKH);

        int GetTotalRec();
    }
}
