using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository.Core
{
    public interface IRepositoryAsync { }
    public interface IRepositoryAsync<TEntity> : IRepositoryAsync where TEntity : IBaseEntity
    {

        Task Add(TEntity entity);
        Task<TEntity> FindById(int id);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
