using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using QLBH_Services.SANPHAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.MEDIA
{
    public class MediaRepository : GenericRepository<Medium>, IMediaRepository
    {
        public MediaRepository(QLBH_ONLINEContext context) : base(context) { }

        List<Medium> IMediaRepository.GetListMedia()
        {
            return _context.Media.
                Include(x => x.MediaType).
                AsNoTracking().
                ToList();
        }

        List<Medium> IMediaRepository.GetBanner()
        {
            return _context.Media.
                    Include(x => x.MediaType).
                    Where(x => x.MediaTypeId == 2 && x.IsActive == true).
                    AsNoTracking().
                    ToList();
        }

        Medium IMediaRepository.GetLogo()
        {
            return _context.Media.
                    Include(x => x.MediaType).
                    OrderByDescending(x => x.CreatedDate).
                    Where(x => (x.MediaTypeId == 1) && (x.IsActive == true)).
                    FirstOrDefault();
        }
    }
}
