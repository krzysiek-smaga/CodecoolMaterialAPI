using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IEduMaterialNavPointRepository EduMaterialNavPoints { get; }
        IEduMaterialTypeRepository EduMaterialTypes { get; }
        IReviewRepository Reviews { get; }

        Task Save();
    }
}
