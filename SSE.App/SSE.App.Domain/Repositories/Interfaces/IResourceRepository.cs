using SSE.App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Domain.Repositories.Interfaces
{
    public interface IResourceRepository
    {
        string? Get(string resourceName, string languageCode);

        void Add(ResourceModel resource);
    }
}
