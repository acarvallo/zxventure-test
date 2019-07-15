using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Core;
using Data.Repository.Core;

namespace Data.Repository.RedisCache
{
    public class RedisRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private RedisContext Context { get; set; }

        private IAppContext AppContext { get; set; }
        public RedisRepository(IDataContext context, IAppContext appContext)
        {
            Context = (RedisContext)context;
            AppContext = appContext;
        }
        public bool Add(TEntity entity)
        {
            var key = GetRedisKey(entity);
            return Context.Database.Add(key, entity);

        }
        private RedisKey GetRedisKey(TEntity entity)
        {
            return $"{typeof(TEntity).Name}{entity.Id}-{Guid.NewGuid().ToString()}";
        }

    }
}
