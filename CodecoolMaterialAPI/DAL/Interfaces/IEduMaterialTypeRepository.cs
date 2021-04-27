using CodecoolMaterialAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Interfaces
{
    public interface IEduMaterialTypeRepository : IRepository<EduMaterialType>
    {
        Task<EduMaterialType> GetEduMaterialTypeByIdWithModels(int id);
        Task<ICollection<EduMaterialType>> GetAllEduMaterialTypesWithModels();
        Task<EduMaterialType> CheckIfEduMaterialTypeExists(EduMaterialType eduMaterialType);
    }
}
