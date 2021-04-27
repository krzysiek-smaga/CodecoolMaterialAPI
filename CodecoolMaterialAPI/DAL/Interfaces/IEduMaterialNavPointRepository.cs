using CodecoolMaterialAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Interfaces
{
    public interface IEduMaterialNavPointRepository : IRepository<EduMaterialNavPoint>
    {
        Task<EduMaterialNavPoint> GetEduMaterialNavPointByIdWithModels(int id);
        Task<ICollection<EduMaterialNavPoint>> GetAllEduMaterialNavPointsWithModels();
        Task<EduMaterialNavPoint> CheckIfEduMaterialNavPointExists(EduMaterialNavPoint eduMaterialNavPoint);
    }
}
