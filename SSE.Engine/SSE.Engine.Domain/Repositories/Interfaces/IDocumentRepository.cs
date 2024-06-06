using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Domain.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        int Add(Guid appId, Datastructures.Document.Document document);
        int AddRange(Guid appId, IEnumerable<Datastructures.Document.Document> document);

        IEnumerable<Datastructures.Document.Document> GetByAppId(Guid appId);
    }
}
