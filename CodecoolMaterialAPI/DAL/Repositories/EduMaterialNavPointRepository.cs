using CodecoolMaterialAPI.DAL.Data;
using CodecoolMaterialAPI.DAL.Interfaces;
using CodecoolMaterialAPI.DAL.Models;
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
    }
}
