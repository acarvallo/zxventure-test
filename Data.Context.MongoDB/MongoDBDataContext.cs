using MongoDB.Driver;
using System;

namespace Data.Context.MongoDB
{
    public class MongoDBDataContext : IDisposable
    {
        protected IMongoClient Client { get; private set; }

        public ContextConfig Config { get; }

        public IMongoDatabase Database { get; set; }

        public MongoDBDataContext(ContextConfig config)
        {
            try
            {
                Config = config;
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(config.ConnectionString));
                Client = new MongoClient(settings);

                Database = GetDatabase();

            }
            catch (Exception ex)
            {
                throw new Exception("Error on connect to MongoDB server.", ex);
            }
        }
        public void CreateCollection(string collectionName)
        {
            Database.CreateCollection(collectionName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            return Database.GetCollection<TEntity>(name);
        }

        private IMongoDatabase GetDatabase()
        {
            try
            {
                return Client.GetDatabase(Config.DataBaseName);

            }
            catch (Exception ex)
            {

                throw new Exception($"Error on getting database {Config.DataBaseName}", ex);
            }
        }

        public void Dispose()
        {
            Client = null;
        }
    }
}
