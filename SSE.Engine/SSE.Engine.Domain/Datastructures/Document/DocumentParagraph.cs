using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Domain.Datastructures.Document
{
    public class DocumentParagraph
    {
        public Guid DocumentId { get; set; }
        public int ParagraphNumber { get; set; }
        public string? Content { get; set; }
        public double? Rate { get; set; }
    }
}
