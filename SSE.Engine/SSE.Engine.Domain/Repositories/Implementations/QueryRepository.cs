using SSE.Engine.Domain.Datastructures.Query;
using SSE.Engine.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Domain.Repositories.Implementations
{
    public class QueryRepository : IQueryRepository
    {

        public IEnumerable<QueryResult> Run(string query,Guid appId)
        {
            throw new NotImplementedException();
        }
    }
}
