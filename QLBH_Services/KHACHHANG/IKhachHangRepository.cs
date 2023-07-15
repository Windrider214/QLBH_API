using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.KHACHHANG
{
    public interface IKhachHangRepository: IGenericRepository<Khachhang>
    {

        Khachhang GetByUserID(string userID);

        List<Khachhang> GetListPaging(int page, int pageSize);

        List<Khachhang> SearchKH(string TenKH);

        int GetTotalRec();


    }
}
