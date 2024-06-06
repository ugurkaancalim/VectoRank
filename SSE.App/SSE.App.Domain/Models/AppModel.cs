using SSE.App.Domain.Models.Types;
using SSE.App.Infrastructure.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Domain.Models
{
    public class AppModel
    {
        public Guid Id { get; set; }

        public DateTime UpdateDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public AppTypeModel Type { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public string? LanguageCode { get; set; }
        public long DataId { get; set; }
    }
}
