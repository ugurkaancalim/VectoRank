using SSE.App.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Domain.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IAppRepository AppRepository { get; }
        public IResourceRepository ResourceRepository { get; }

        int SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
