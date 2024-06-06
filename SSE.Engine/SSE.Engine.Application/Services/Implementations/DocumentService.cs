using MongoDB.Driver;
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
    public class DocumentService : IDocumentService
    {
        private readonly IAppService _appService;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IVectorDataService _vectorDataService;
        public DocumentService(IAppService appService, IVectorDataService vectorDataService)
        {
            _appService = appService;
            var mongoInfo = _appService.GetDatabaseInfo();
            _mongoClient = new MongoClient(mongoInfo.connectionString);
            _mongoDatabase = _mongoClient.GetDatabase(mongoInfo.databaseName);
            _vectorDataService = vectorDataService;
        }
        public async Task AddDocument(DocumentDto document)
        {
            //Add paragraphs
            //Add sentences
            int pNumber = 0;
            var paragraphItems = new List<DocumentParagraph>();
            var sentenceItems = new List<DocumentSentence>();
            var paragraphs = document.Content?.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var paragraph in paragraphs)
            {
                pNumber++;
                var paragraphItem = new DocumentParagraph()
                {
                    Id = Guid.NewGuid(),
                    Content = paragraph,
                    DocumentId = document.Id,
                    Labels = document.Labels,
                    ParagraphNumber = pNumber,
                    StartIndex = document.Content.IndexOf(paragraph),
                    Rank = 0
                };
                double totalRank = 0;
                int sNumber = 0;
                var sentences = VectoRank.Text.SentenceHelper.GetSentences(paragraph);
                foreach (var sentence in sentences)
                {
                    sNumber++;
                    var tokens = VectoRank.Tokenizers.VectoRankTokenizer.Tokenize(sentence);
                    var sentenceVectors = await _vectorDataService.GetVectors(tokens);
                    var sentenceRank = VectoRank.Rankers.Ranker.Rank(sentenceVectors);
                    //add sentence
                    totalRank += sentenceRank;
                    sentenceItems.Add(new DocumentSentence()
                    {
                        Content = sentence,
                        ParagraphId = paragraphItem.Id,
                        Rank = sentenceRank,
                        SentenceNumber = sNumber,
                        StartIndex = paragraph.IndexOf(sentence),
                        DocumentId = document.Id
                    });
                }
                paragraphItem.Rank = totalRank / sentences.Length;
                //AddDocument paragraph
                paragraphItems.Add(paragraphItem);
            }
            var paragraphCollection = _mongoDatabase.GetCollection<DocumentParagraph>(DocumentConstants.ParagraphsTable);
            var sentenceCollection = _mongoDatabase.GetCollection<DocumentSentence>(DocumentConstants.SentencesTable);
            paragraphCollection.InsertMany(paragraphItems);
            sentenceCollection.InsertMany(sentenceItems);
        }

        public async Task AddDocuments(DocumentDto[] documents)
        {
            foreach (var document in documents)
            {
                await AddDocument(document);
            }
        }


        private async Task<bool> CheckIfCollectionExists(string dbName)
        {
            // List all collection names
            var collNames = await _mongoDatabase.ListCollectionNamesAsync().Result.ToListAsync();
            // Check if the collection you're interested in exists
            return collNames.Contains(dbName);
        }
    }
}
