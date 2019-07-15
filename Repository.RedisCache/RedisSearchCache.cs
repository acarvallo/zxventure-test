using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiclePlot.Data.Repository.Core;

namespace VehiclePlot.Data.Repository.RedisCache
{
    public class RedisSearchCache<TEntity> : IQueryAsync<TEntity> where TEntity : BaseEntity
    {
        private RedisContext Context { get; set; }

        public RedisSearchCache(IDataContext context)
        {
            Context = (RedisContext)context;
        }

        public async Task<IEnumerable<TEntity>> Find(int id, Func<TEntity, bool> predicate)
        {
            try
            {
                var allKey = Context.Database.SearchKeys($"{typeof(TEntity).Name}{id}-*");
                var all = Context.Database.GetAll<TEntity>(allKey);

                return all.Select(p => p.Value).Where(predicate);
            }
            catch (Exception ex)
            {

                throw new Exception("Error on retrive from redis");
            }

        }
    }
}

