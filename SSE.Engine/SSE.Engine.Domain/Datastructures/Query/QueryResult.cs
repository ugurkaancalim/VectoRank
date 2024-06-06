using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Domain.Datastructures.Query
{
    public class QueryResult
    {
        public string? DocumentId { get; set; }
        public int NumberOfParagraph { get; set; }
        public string? Text { get; set; }
        public int StartIndex { get; set; }
        public int Length { get; set; }
    }
}
