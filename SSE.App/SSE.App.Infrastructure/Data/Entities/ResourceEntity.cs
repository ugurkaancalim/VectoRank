using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Infrastructure.Data.Entities
{
    public class ResourceEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? LanguageCode { get; set; }
    }
}
