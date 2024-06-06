using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Domain.Datastructures.Document
{
    public class Document
    {
        public Guid ApplicationId { get; set; }
        public string? ApplicationDocumentId { get; set; }
        public string? ApplicationDocumentName { get; set; }
        public DateTime AddDate { get; set; }
        public IEnumerable<DocumentParagraph> Paragraphs { get; set; }
        public Dictionary<string,string> Labels { get; set; }
    }
}
