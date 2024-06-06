using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Application.DTOs
{
    public class DocumentDto
    {
        public string? Id { get; set; }
        public string? Content { get; set; }
        public Dictionary<string,string>? Labels { get; set; }
    }

}
