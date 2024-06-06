using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Application.DataStructures.Document.Document
{
    public class DocumentParagraph
    {
        public Guid Id { get; set; }
        public string? DocumentId { get; set; }
        public int? ParagraphNumber { get; set; }
        public double Rank { get; set; }
        public string? Content { get; set; }
        public Dictionary<string, string>? Labels { get; set; }
        public int StartIndex { get; set; }
    }
}
