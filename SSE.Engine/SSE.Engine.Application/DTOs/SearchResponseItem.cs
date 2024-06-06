using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Application.DTOs
{
    public class SearchResponseItem
    {
        public string? DocumentId { get; set; }
        public string? Content { get; set; }
        public int? StartIndex { get; set; }
    }

}
