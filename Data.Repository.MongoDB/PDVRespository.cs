
using Data.Context.MongoDB;
using Data.Entities;
using Data.Entities.Common;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository.MongoDB
{
    public class PDVRespository : IPDVRepository
    {
        private const string TypeName = "pdvs";
        public MongoDBDataContext Context { get; set; }
        public PDVRespository(MongoDBDataContext context)
        {
            Context = context;
        }
        public async Task Add(PDVEntity entity)
        {
            var collection = Context.GetCollection<PDVEntity>(TypeName);

            entity.id = await GenerateId();
            await Context.GetCollection<PDVEntity>(TypeName).InsertOneAsync(entity);
        }

        private async Task<int> GenerateId()
        {
            var newId = new ObjectId();
            newId.Id = TypeName;

            var colObjecId = Context.GetCollection<ObjectId>("ObjectId");

            var result = await colObjecId.FindAsync(p => p.Id == TypeName);
            var objId = result.FirstOrDefault();

            if (objId == null)
            {
                var pdvs = await Context.GetCollection<PDVEntity>(TypeName).FindAsync(p => p.id > 0);
                var maxId = pdvs.ToList().Max(p => p.id);
                newId.Sequence = maxId + 1;

                await colObjecId.InsertOneAsync(newId);
            }
            else
            {
                newId.Sequence = objId.Sequence + 1;
                await colObjecId.FindOneAndReplaceAsync(p => p.Id == newId.Id, newId);
            }
            return newId.Sequence;
        }
        public async Task<PDVEntity> FindById(int id)
        {
            var ret = await Find(p => p.id == id);
            return ret.FirstOrDefault();
        }

        public async Task<IEnumerable<PDVEntity>> Find(Expression<Func<PDVEntity, bool>> predicate)
        {
            var list = await Context.GetCollection<PDVEntity>(TypeName).FindAsync(predicate);

            return list.ToList();
        }

        public async Task<PDVEntity> GetNearst(double longt, double lat)
        {
            var coord = new GeoJson2DCoordinates(longt, lat);
            var geo = new GeoJsonPoint<GeoJson2DCoordinates>(coord);

            var filterBuiler = new FilterDefinitionBuilder<PDVEntity>();
            var filterInAreas = filterBuiler.GeoIntersects(p => p.coverageArea, geo);
            var filterNear = filterBuiler.Near(p => p.address, geo);

            filterBuiler.And(filterInAreas, filterNear);

            var collection = Context.GetCollection<PDVEntity>(TypeName);
            var indexKey = new IndexKeysDefinitionBuilder<PDVEntity>().Geo2DSphere(p => p.address);
            var index = new CreateIndexModel<PDVEntity>(indexKey);

            collection.Indexes.CreateOne(index);

            var list = await Context.GetCollection<PDVEntity>(TypeName).FindAsync(filterInAreas);

            return list.FirstOrDefault();
        }
    }
}
