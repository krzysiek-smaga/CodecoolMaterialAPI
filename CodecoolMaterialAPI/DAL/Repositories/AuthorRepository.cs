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
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(CodecoolMaterialDbContext context) : base(context)
        {
        }

        public async Task<Author> GetAuthorByIdWithModels(int id)
        {
            return await _context.Authors
                .Include(x => x.EduMaterialNavPoints)
                .Include(x => x.EduMaterialNavPoints)
                    .ThenInclude(x => x.EduMaterialType)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task<ICollection<Author>> GetAllAuthorsWithModels()
        {
            return await _context.Authors
                .Include(x => x.EduMaterialNavPoints)
                .Include(x => x.EduMaterialNavPoints)
                    .ThenInclude(x => x.EduMaterialType)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
