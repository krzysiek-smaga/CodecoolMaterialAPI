using CodecoolMaterialAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<Review> GetReviewByIdWithModels(int id);
        Task<ICollection<Review>> GetAllReviewsWithModels();
    }
}
