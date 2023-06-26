﻿using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.TINTUC
{
    public class BlogRepository : GenericRepository<Tintuc>, IBlogRepository
    {
        public BlogRepository(QLBH_ONLINEContext context) : base(context) { }

        public List<Tintuc> GetBlogPaging(int page, int pageSize)
        {
            return _context.Tintucs.
                     Include(x => x.MaDmNavigation).
                     OrderByDescending(y => y.NgayDang).
                     Include(z => z.User).
                     Skip((page - 1) * pageSize).Take(pageSize).
                     AsNoTracking().
                     ToList();
        }

        public int GetTotalRec()
        {
            return _context.Tintucs.
                   Include(x => x.MaDmNavigation).
                   ToList().Count();
        }

        public List<Tintuc> SearchBlog(string maTinTuc)
        {
          return   _context.Tintucs.
                    Include(x => x.MaDmNavigation).
                    Include(z => z.User).
                    Where(x => x.MaTinTuc == maTinTuc).
                    AsNoTracking().
                    ToList();
        }

        public Tintuc GetBlogSaleNews()
        {
            return _context.Tintucs.
                 Include(x => x.MaDmNavigation).
                 Include(z => z.User).
                 Where(x => x.MaDm == "6d8d5228-27bb-4e37-9916-5d1b21e5c04f").
                 OrderByDescending(y => y.NgayDang).
                 AsNoTracking().
                 FirstOrDefault();
        }

    }
}
