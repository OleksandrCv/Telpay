using System.Collections.Generic;
using System.Threading.Tasks;

namespace Telpay.Business
{
    public interface IRepository<TEntity, in TKey> where TEntity : class, IDbEntity<TKey> where TKey : struct
    {
        TEntity Get(TKey id);
        IList<TEntity> GetAll();
        void Save(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);
        void Delete(TEntity entity);
        void Commit();
        Task CommitAsync();
    }
}
