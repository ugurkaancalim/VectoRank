using SSE.App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Services.Interfaces
{
    public interface IAppService
    {
        AppModel? GetById(Guid id);
        IEnumerable<AppModel> GetAll();
        Task Add(AppModel model);
        void Update(AppModel model);
        void Remove(AppModel model);
    }
}
