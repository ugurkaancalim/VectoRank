using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Infrastructure.Data.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime UpdateDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
