using SSE.App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Services.Interfaces
{
    public interface IEngineService
    {
        Task<bool> AddApplication(AppModel app);
    }
}
