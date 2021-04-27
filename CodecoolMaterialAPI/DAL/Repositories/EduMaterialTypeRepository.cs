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
    public class EduMaterialTypeRepository : Repository<EduMaterialType>, IEduMaterialTypeRepository
    {
        public EduMaterialTypeRepository(CodecoolMaterialDbContext context) : base(context)
        {
        }

        public async Task<EduMaterialType> GetEduMaterialTypeByIdWithModels(int id)
        {
            return await _context.EduMaterialTypes
                .Include(x => x.EduMaterialNavPoints)
                .Include(x => x.EduMaterialNavPoints)
                    .ThenInclude(x => x.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task<ICollection<EduMaterialType>> GetAllEduMaterialTypesWithModels()
        {
            return await _context.EduMaterialTypes
                .Include(x => x.EduMaterialNavPoints)
                .Include(x => x.EduMaterialNavPoints)
                    .ThenInclude(x => x.Author)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EduMaterialType> CheckIfEduMaterialTypeExists(EduMaterialType eduMaterialType)
        {
            return await _context.EduMaterialTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name == eduMaterialType.Name);
        }
    }
}
