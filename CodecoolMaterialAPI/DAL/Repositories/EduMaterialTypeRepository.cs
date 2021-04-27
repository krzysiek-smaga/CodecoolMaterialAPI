using CodecoolMaterialAPI.DAL.Data;
using CodecoolMaterialAPI.DAL.Interfaces;
using CodecoolMaterialAPI.DAL.Models;
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
    }
}
