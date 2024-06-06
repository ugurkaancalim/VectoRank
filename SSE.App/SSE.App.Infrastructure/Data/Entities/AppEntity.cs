using SSE.App.Infrastructure.Data.Entities.Base;
using SSE.App.Infrastructure.Data.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Infrastructure.Data.Entities
{
    public class AppEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public AppType Type { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public string? LanguageCode { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DataId { get; set; }
    }
}
