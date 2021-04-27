using CodecoolMaterialAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> GetAuthorByIdWithModels(int id);
    }
}
