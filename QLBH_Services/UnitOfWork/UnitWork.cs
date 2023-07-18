using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_Services.HOADON;
using QLBH_Services.HOADON.HOADONCT;
using QLBH_Services.KHACHHANG;
using QLBH_Services.MEDIA;
using QLBH_Services.MEDIA.MEDIATYPE;
using QLBH_Services.PHANHOI;
using QLBH_Services.SANPHAM;
using QLBH_Services.SANPHAM.LOAISP;
using QLBH_Services.SANPHAM.THUONGHIEU;
using QLBH_Services.STATISTIC;
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
    public class UnitWork : IUnitWork
    {
        private readonly QLBH_ONLINEContext _context;

        public SanPhamRepository _spRepos;
        public LoaiSPRepository _loaiSPRepos;
        public ThuongHieuRepository _thuongHieuRepo;

        #region oldRepos
        //public SanPhamRepository spRepos
        //{
        //    get
        //    {
        //        if (_spRepos == null)
        //        {
        //            _spRepos = new SanPhamRepository(_context);
        //        }
        //        return _spRepos;
        //    }
        //}

        //public LoaiSPRepository LoaiSPRepository
        //{
        //    get
        //    {
        //        if (_loaiSPRepos == null)
        //        {
        //            _loaiSPRepos = new LoaiSPRepository(_context);
        //        }
        //        return _loaiSPRepos;
        //    }
        //}

        //public ThuongHieuRepository ThuongHieuRepository
        //{
        //    get
        //    {
        //        if (_thuongHieuRepo == null)
        //        {
        //            _thuongHieuRepo = new ThuongHieuRepository(_context);
        //        }
        //        return _thuongHieuRepo;
        //    }
        //}
        #endregion

        public UnitWork(QLBH_ONLINEContext context)
        {
            _context = context;
            LoaiSPRepository = new LoaiSPRepository(_context);
            thuongHieuRepository = new ThuongHieuRepository(_context);
            SanPhamRepository = new SanPhamRepository(_context);
            MediaRepository = new MediaRepository(_context);
            MediaTypeRepository = new MediaTypeRepository(_context);
            BlogTypeRepository = new BlogTypeRepositoy(_context);
            BlogRepository = new BlogRepository(_context);
            UserRepository = new UserRepository(_context);
            HoaDonRepository = new HoaDonRepository(_context);
            HoaDonCTRepository = new HoaDonCTRepository(_context);
            KhachHangRepository = new KhachHangRepository(_context);
            RoleRepository = new RoleRepository(_context);
            PhanHoiRepository = new PhanHoiRepository(_context);
            ThongKeLoiNhuanRepository = new ThongKeLoiNhuanRepository(_context);
        }

        public ILoaiSPRepository LoaiSPRepository
        {
            get;
            private set;
        }

        public IThuongHieuRepository thuongHieuRepository
        {
            get;
            private set;
        }

        public ISanPhamRepository SanPhamRepository 
        { 
            get;
            private set;
        }

        public IMediaRepository MediaRepository { 
            get;
            private set;
        }

        public IMediaTypeRepository MediaTypeRepository 
        { 
            get; 
            private set; 
        }

        public IBlogTypeRepository BlogTypeRepository
        {
            get;
            private set;
        }

        public IBlogRepository BlogRepository 
        { 
            get;
            private set;
        }

        public IUserRepository UserRepository
        {
            get;
            private set;
        }

        public IHoaDonRepository HoaDonRepository
        {
            get;
            private set;
        }

        public IHoaDonCTRepository HoaDonCTRepository
        {
            get;
            private set;
        }

        public IKhachHangRepository KhachHangRepository
        {
            get;
            private set;
        }

        public IRoleRepository RoleRepository
        {
            get;
            private set;
        }

        public IPhanHoiRepository PhanHoiRepository
        {
            get;
            private set;
        }

        public IThongKeLoiNhuanRepository ThongKeLoiNhuanRepository
        {
            get;
            private set;
        }

        public void Dispose()
        {
            _context.Dispose();
        }



        public int Save()
        {

            return _context.SaveChanges();
        }

      

    }
}
