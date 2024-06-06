using SSE.Engine.Application.DataStructures.App;
using SSE.Engine.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using SSE.Engine.Application.Constants;
using System.Data.Common;
using SSE.Engine.Application.DataStructures.Document.Document;
using System.Runtime.CompilerServices;
using SSE.Engine.Application.DataStructures.Document;

namespace SSE.Engine.Application.Services.Implementations
{
    public class AppService : IAppService
    {

        private readonly IMongoClient _client;
        private readonly IClaimService _claimService;
        public AppService(IClaimService claimService)
        {
            _client = new MongoClient(ConnectionConstants.AppsConnectionString);
            _claimService = claimService;
        }
        public async Task Add(long dataId, string languageCode)
        {
            var app = new AppInfo()
            {
                DataId = dataId,
                AppId = _claimService.AppClaims.AppId,
                LanguageCode = languageCode
            };
            bool createIndexes = false;
            if (await CheckIfDatabaseExists(AppDatabaseConstants.DatabaseName))
            {
                createIndexes = true;
            }
            var database = _client.GetDatabase(AppDatabaseConstants.DatabaseName);
            var collection = database.GetCollection<AppInfo>(AppDatabaseConstants.TableName);
            collection.InsertOne(app);
            if (createIndexes) CreateIndexes(database);
            CreateAppTables(app.DataId);
        }
        private void CreateAppTables(long? dataId)
        {
            var dbInfo = getInfoByDataId(dataId);
            var creatorClient = new MongoClient(dbInfo.connectionString);
            var creatorDatabase = creatorClient.GetDatabase(dbInfo.databaseName);
            var paragraph = creatorDatabase.GetCollection<DocumentParagraph>(DocumentConstants.ParagraphsTable);
            var sentence = creatorDatabase.GetCollection<DocumentSentence>(DocumentConstants.SentencesTable);
            //create indexes
            CreateRankIndexes(creatorDatabase, paragraph);
            CreateRankIndexes(creatorDatabase, sentence);
        }
        private void CreateRankIndexes<T>(IMongoDatabase database, IMongoCollection<T> coll)
        {
            var indexKeys = Builders<T>.IndexKeys.Ascending("Rank");
            var indexOptions = new CreateIndexOptions { Unique = false }; // Optional: Set unique to true for unique indexes
            var indexModel = new CreateIndexModel<T>(indexKeys, indexOptions);
            coll.Indexes.CreateOne(indexModel);
        }
        public (string connectionString, string databaseName) GetDatabaseInfo()
        {
            var database = _client.GetDatabase(AppDatabaseConstants.DatabaseName);
            var filter = Builders<AppInfo>.Filter.Eq(x => x.AppId, _claimService.AppClaims.AppId);
            var collection = database.GetCollection<AppInfo>(AppDatabaseConstants.TableName);
            var app = collection.Find(filter).FirstOrDefault();
            if (app != null)
            {
                return getInfoByDataId(app.DataId);
            }
            throw new Exception("App not found");
        }
        private (string connectionString, string databaseName) getInfoByDataId(long? dataId)
        {
            var address = dataId.ToString().PadLeft(11, '0');
            return (ConnectionConstants.ConnectionStrings[address.Substring(0, 9)],
                 "T" + address);
        }

        private async Task<bool> CheckIfDatabaseExists(string dbName)
        {
            // List all database names
            var databaseNames = await _client.ListDatabaseNamesAsync().Result.ToListAsync();

            // Check if the database you're interested in exists
            return databaseNames.Contains(dbName);
        }
        /// <summary>
        /// Create indexes if creating first app.
        /// </summary>
        /// <param name="database"></param>
        private void CreateIndexes(IMongoDatabase database)
        {
            var indexKeys = Builders<AppInfo>.IndexKeys.Ascending("AppId");
            var indexOptions = new CreateIndexOptions { Unique = false }; // Optional: Set unique to true for unique indexes
            var indexModel = new CreateIndexModel<AppInfo>(indexKeys, indexOptions);
            var collection = database.GetCollection<AppInfo>(AppDatabaseConstants.TableName);
            collection.Indexes.CreateOne(indexModel);
        }

    }
}
