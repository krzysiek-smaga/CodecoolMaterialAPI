using CodecoolMaterialAPI.DAL.Data;
using CodecoolMaterialAPI.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CodecoolMaterialDbContext _context;

        public UnitOfWork(CodecoolMaterialDbContext context)
        {
            _context = context;
            Authors = new AuthorRepository(_context);
            EduMaterialNavPoints = new EduMaterialNavPointRepository(_context);
            EduMaterialTypes = new EduMaterialTypeRepository(_context);
            Reviews = new ReviewRepository(_context);

        }

        public IAuthorRepository Authors { get; private set; }
        public IEduMaterialNavPointRepository EduMaterialNavPoints { get; private set; }
        public IEduMaterialTypeRepository EduMaterialTypes { get; private set; }
        public IReviewRepository Reviews { get; private set; }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
