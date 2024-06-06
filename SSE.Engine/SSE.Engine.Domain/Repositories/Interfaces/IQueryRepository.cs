using SSE.Engine.Domain.Datastructures.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Domain.Repositories.Interfaces
{
    public interface IQueryRepository
    {
        IEnumerable<QueryResult> Run(string query, Guid appId);
    }
}
