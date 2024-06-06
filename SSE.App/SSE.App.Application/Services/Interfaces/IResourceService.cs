using SSE.App.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Services.Interfaces
{
    public interface IResourceService
    {
        string? Get(ResourceNames resourceName);
        string? GetLanguageCode();
    }
}
