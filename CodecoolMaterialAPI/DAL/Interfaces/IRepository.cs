using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);
        Task<ICollection<TEntity>> GetAll();
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
