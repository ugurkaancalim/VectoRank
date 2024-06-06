using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using SSE.Engine.Application.Constants;
using SSE.Engine.Application.DataStructures.Document.Document;
using SSE.Engine.Application.DTOs;
using SSE.Engine.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectoRank = SSE.Core.VectoRank;

namespace SSE.Engine.Application.Services.Implementations
{
    public class QueryService : IQueryService
    {
        private readonly IAppService _appService;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IVectorDataService _vectorDataService;
        private static double range = 0.01;
        public QueryService(IAppService appService, IVectorDataService vectorDataService)
        {
            _appService = appService;
            var mongoInfo = _appService.GetDatabaseInfo();
            _mongoClient = new MongoClient(mongoInfo.connectionString);
            _mongoDatabase = _mongoClient.GetDatabase(mongoInfo.databaseName);
            _vectorDataService = vectorDataService;
        }
        public async Task<SearchResponseDto> Execute(string query)
        {
            //avg sentence length:10-15
            var tokens = VectoRank.Tokenizers.VectoRankTokenizer.Tokenize(query);
            var sentenceVectors = await _vectorDataService.GetVectors(tokens);
            var sentenceRank = VectoRank.Rankers.Ranker.Rank(sentenceVectors);
            if (tokens.Count < 30)
            {
                return new SearchResponseDto()
                {
                    Result = await GetSentenceResults(sentenceRank)
                };
            }
            else
            {
                return new SearchResponseDto()
                {
                    Result = await GetParagraphResults(sentenceRank)
                };
            }
            return null;
        }
        private async Task<List<SearchResponseItem>> GetParagraphResults(double rank)
        {
            var filter = Builders<DocumentParagraph>.Filter.And(
    Builders<DocumentParagraph>.Filter.Gte(x => x.Rank, rank - range), // Adjust range accordingly
    Builders<DocumentParagraph>.Filter.Lte(x => x.Rank, rank + range)
);
            var projection = Builders<DocumentParagraph>.Projection.Include(x => x.Rank);
            var sort = Builders<DocumentParagraph>.Sort.Ascending(x => Math.Abs(x.Rank - rank));

            var closestValues = await _mongoDatabase.GetCollection<DocumentParagraph>(DocumentConstants.ParagraphsTable).Find(filter)
                                                .Project<DocumentParagraph>(projection)
                                                .Sort(sort)
                                                .Limit(10)
                                                .ToListAsync()
                                                ;
            return closestValues.Select(x => new SearchResponseItem()
            {
                Content = x.Content,
                DocumentId = x.DocumentId,
                StartIndex = x.StartIndex
            }).ToList();
        }
        private async Task<List<SearchResponseItem>> GetSentenceResults(double rank)
        {
            var filter = Builders<DocumentSentence>.Filter.And(
    Builders<DocumentSentence>.Filter.Gte(x => x.Rank, rank - range), // Adjust range accordingly
    Builders<DocumentSentence>.Filter.Lte(x => x.Rank, rank + range)
);
            var projection = Builders<DocumentSentence>.Projection.Include(x => x.Rank);
            var sort = Builders<DocumentSentence>.Sort.Ascending(x => Math.Abs(x.Rank - rank));

            var closestValues = await _mongoDatabase.GetCollection<DocumentSentence>(DocumentConstants.ParagraphsTable).Find(filter)
                                                .Project<DocumentSentence>(projection)
                                                .Sort(sort)
                                                .Limit(10)
                                                .ToListAsync()
                                                ;
            return closestValues.Select(x => new SearchResponseItem()
            {
                Content = x.Content,
                DocumentId = x.DocumentId,
                StartIndex = x.StartIndex
            }).ToList();
        }

    }
}
