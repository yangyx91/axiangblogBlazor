using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service
{
    public class Dao : IDisposable
    {
        private string dbName = "ApiDB";
        private string collectionName = "BingImgs";

        private bool disposed = false;

        # region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }

            this.disposed = true;
        }

        # endregion

        private static MongoClient mongoClient = new MongoClient();

        private static IMongoDatabase mongoDatabase;

        public Dao()
        {
            string connectionString = @"mongodb://axiangmongodb:pzn4l3I6TUAs1YWmFZIvd3aLNozPMxyhl2UzoQLeUVL6IWJsjhsSyPlCwFAJUPghpq8bKMG8f154WybOPHNHaA==@axiangmongodb.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@axiangmongodb@&retrywrites=false";
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            mongoClient = new MongoClient(settings);
            mongoDatabase = mongoClient.GetDatabase(dbName);
        }

        #region 初始化Mongodb

        public List<string> GetDatabaseNames()
        {
            var result = new List<string>();
            try
            {
                var databases = mongoClient.ListDatabases().ToList();
                databases.ForEach(b =>
                {
                    try
                    {
                        result.Add(b["name"].AsString);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }


        public async Task<IEnumerable<string>> GetCollectionNames()
        {
            var result = new List<string>();
            var collectionNames = mongoDatabase.ListCollectionNames().ToList();

            collectionNames.ForEach(cBson =>
            {
                try
                {
                    result.Add(cBson);
                }
                catch (Exception e)
                {

                    throw e;
                }
            });

            return result;
        }

        #endregion

        #region 增删改

        public async Task AddOneDocument<T>(T document)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collectionName);
            await mongoCollection.InsertOneAsync(document);
        }

        public void AddManyDocument<T>(List<T> documents)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collectionName);
            mongoCollection.InsertMany(documents);
        }

        public void CountAllDocuments<T>()
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collectionName);
            //mongoCollection.CountDocuments(new FilterDefinition<>());
        }

        public async Task<List<T>> FindDocuments<T>(string conditionName, string conditionValue)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq(conditionName, conditionValue);
            return await mongoCollection.Find<T>(filter).ToListAsync();
        }

        public async Task<List<T>> FindDocumentsByPage<T>(string conditionName, string conditionValue, string sortName, int page = 1, int pageSize = 10)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq(conditionName, conditionValue);
            var sortFilter = Builders<T>.Sort.Descending(sortName);
            return await mongoCollection.Find<T>(filter).Sort(sortFilter).Skip((page - 1) * pageSize).Limit(50).ToListAsync();
        }

        public long CountDocuments<T>(string conditionName, string conditionValue)
        {
            var mongoCollection = mongoDatabase.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq(conditionName, conditionValue);
            return mongoCollection.Find<T>(filter).CountDocuments();
        }
        #endregion
    }
}

