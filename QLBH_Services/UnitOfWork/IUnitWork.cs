using QLBH_Services.HOADON;
using QLBH_Services.HOADON.HOADONCT;
using QLBH_Services.KHACHHANG;
using QLBH_Services.MEDIA;
using QLBH_Services.MEDIA.MEDIATYPE;
using QLBH_Services.SANPHAM;
using QLBH_Services.SANPHAM.LOAISP;
using QLBH_Services.SANPHAM.THUONGHIEU;
using QLBH_Services.TINTUC;
using QLBH_Services.TINTUC.LOAITIN;
using QLBH_Services.USER;
using QLBH_Services.USER.ROLES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.UnitOfWork
{
    public interface IUnitWork : IDisposable
    {
        ILoaiSPRepository LoaiSPRepository { get; }

        IThuongHieuRepository thuongHieuRepository { get; }

        ISanPhamRepository SanPhamRepository { get; }

        IMediaRepository MediaRepository { get; }

        IMediaTypeRepository MediaTypeRepository { get; }

        IBlogTypeRepository BlogTypeRepository { get; }

        IBlogRepository BlogRepository { get; }

        IUserRepository UserRepository { get; }

        IHoaDonRepository HoaDonRepository { get; }

        IHoaDonCTRepository HoaDonCTRepository { get; }

        IKhachHangRepository KhachHangRepository { get; }

        IRoleRepository RoleRepository { get; }
        #region oldRepos
        //SanPhamRepository spRepos { get; }
        //LoaiSPRepository LoaiSPRepository { get; }
        //ThuongHieuRepository ThuongHieuRepository { get; }
        #endregion

        int Save();
    }
}
