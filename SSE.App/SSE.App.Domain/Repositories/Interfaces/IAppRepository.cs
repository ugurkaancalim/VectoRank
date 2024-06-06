using SSE.App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Domain.Repositories.Interfaces
{
    public interface IAppRepository
    {
        IEnumerable<AppModel> GetByUserId(Guid userId);
        AppModel? GetById(Guid id);
        IEnumerable<AppModel> GetAll();
        AppModel Add(AppModel model);
        void Update(AppModel model);
        void Remove(AppModel model);
    }
}
