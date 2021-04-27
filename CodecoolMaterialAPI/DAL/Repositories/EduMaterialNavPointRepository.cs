using CodecoolMaterialAPI.DAL.Data;
using CodecoolMaterialAPI.DAL.Interfaces;
using CodecoolMaterialAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Repositories
{
    public class EduMaterialNavPointRepository : Repository<EduMaterialNavPoint>, IEduMaterialNavPointRepository
    {
        public EduMaterialNavPointRepository(CodecoolMaterialDbContext context) : base(context)
        {
        }

        public async Task<EduMaterialNavPoint> GetEduMaterialNavPointByIdWithModels(int id)
        {
            return await _context.EduMaterialNavPoints
               .Include(x => x.Author)
               .Include(x => x.EduMaterialType)
               .Include(x => x.Reviews)
               .AsNoTracking()
               .FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task<ICollection<EduMaterialNavPoint>> GetAllEduMaterialNavPointsWithModels()
        {
            return await _context.EduMaterialNavPoints
                .Include(x => x.Author)
                .Include(x => x.EduMaterialType)
                .Include(x => x.Reviews)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EduMaterialNavPoint> CheckIfEduMaterialNavPointExists(EduMaterialNavPoint eduMaterialNavPoint)
        {
            return await _context.EduMaterialNavPoints
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Title == eduMaterialNavPoint.Title &&
                    a.AuthorID == eduMaterialNavPoint.AuthorID &&
                    a.EduMaterialTypeID == eduMaterialNavPoint.EduMaterialTypeID);
        }
    }
}
