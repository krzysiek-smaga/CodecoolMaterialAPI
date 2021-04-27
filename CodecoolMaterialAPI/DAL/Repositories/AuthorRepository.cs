using CodecoolMaterialAPI.DAL.Data;
using CodecoolMaterialAPI.DAL.Interfaces;
using CodecoolMaterialAPI.DAL.Models;
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
    }
}
