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
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(CodecoolMaterialDbContext context) : base(context)
        {
        }

        public async Task<Review> GetReviewByIdWithModels(int id)
        {
            return await _context.Reviews
                .Include(x => x.EduMaterialNavPoint)
                .Include(x => x.EduMaterialNavPoint)
                    .ThenInclude(x => x.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<ICollection<Review>> GetAllReviewsWithModels()
        {
            return await _context.Reviews
                .Include(x => x.EduMaterialNavPoint)
                .Include(x => x.EduMaterialNavPoint)
                    .ThenInclude(x => x.Author)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
