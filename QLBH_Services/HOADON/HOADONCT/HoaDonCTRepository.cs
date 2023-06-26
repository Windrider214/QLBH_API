using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.HOADON.HOADONCT
{
    public class HoaDonCTRepository : GenericRepository<Hoadonct>, IHoaDonCTRepository
    {
        public HoaDonCTRepository(QLBH_ONLINEContext context) : base(context) { }
    }
}
