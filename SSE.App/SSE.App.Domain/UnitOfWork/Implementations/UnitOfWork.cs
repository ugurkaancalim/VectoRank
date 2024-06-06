using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SSE.App.Domain.Repositories.Implementations;
using SSE.App.Domain.Repositories.Interfaces;
using SSE.App.Domain.UnitOfWork.Interfaces;
using SSE.App.Infrastructure.Data;
using SSE.App.Infrastructure.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Domain.UnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction _dbTransaction;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IAppRepository _appRepository;
        public IAppRepository AppRepository
        {
            get
            {
                if (_appRepository == null) _appRepository = new AppRepository(_dbContext);
                return _appRepository;
            }
        }
      
        private IResourceRepository _resourceRepository;
        public IResourceRepository ResourceRepository
        {
            get
            {
                if (_resourceRepository == null) _resourceRepository = new ResourceRepository(_dbContext);
                return _resourceRepository;
            }
        }

        public void BeginTransaction()
        {
            _dbTransaction = _dbContext.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _dbTransaction.Commit();
            SaveChanges();
        }
        public void RollbackTransaction()
        {
            _dbTransaction.Rollback();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public int SaveChanges()
        {
            SoftDeleteEntities();
            return _dbContext.SaveChanges();
        }
        private void SoftDeleteEntities()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Deleted)
                {
                    var type = entry.Entity.GetType();
                    bool extendsBaseEntity = IsSubclassOfBaseEntity(type);
                    if (extendsBaseEntity)
                    {
                        entry.State = EntityState.Modified;
                        ((BaseEntity)entry.Entity).IsDeleted = true;
                    }
                }
            }
        }
        private static bool IsSubclassOfBaseEntity(Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (typeof(BaseEntity) == cur)
                    return true;
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}
