using SSE.Engine.Domain.Datastructures.Document;
using SSE.Engine.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Domain.Repositories.Implementations
{
    public class DocumentRepository : IDocumentRepository
    {
        public int Add(Guid appId, Document document)
        {
            throw new NotImplementedException();
        }

        public int AddRange(Guid appId, IEnumerable<Document> document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Document> GetByAppId(Guid appId)
        {
            throw new NotImplementedException();
        }
    }
}
