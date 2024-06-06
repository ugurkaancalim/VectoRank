using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Application.DataStructures.Document.Document
{
    public class DocumentSentence
    {
        public double Rank { get; set; }
        public Guid ParagraphId { get; set; }
        public int SentenceNumber { get; set; }
        public string? Content { get; set; }
        public string? DocumentId { get; set; }
        public int StartIndex { get; set; }
    }
}
