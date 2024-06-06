using AutoMapper;
using SSE.App.Domain.Mapping;
using SSE.App.Domain.Models;
using SSE.App.Domain.Repositories.Interfaces;
using SSE.App.Infrastructure.Data;
using SSE.App.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Domain.Repositories.Implementations
{
    public class AppRepository : IAppRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public AppRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = MapperCreator.Mapper;
        }

        public AppModel Add(AppModel model)
        {
            var entity = _mapper.Map<AppEntity>(model);
            _dbContext.Apps.Add(entity);
            entity.DataId = _dbContext.Entry(entity).Property(e => e.DataId).CurrentValue;
            return _mapper.Map<AppModel>(entity);
        }
        public IEnumerable<AppModel> GetAll()
        {
            return _mapper.Map<IEnumerable<AppModel>>(_dbContext.Apps.Where(x => !x.IsDeleted));
        }

        public AppModel? GetById(Guid id)
        {
            return _mapper.Map<AppModel>(_dbContext.Apps.FirstOrDefault(a => a.Id == id && !a.IsDeleted));
        }

        public IEnumerable<AppModel> GetByUserId(Guid userId)
        {
            return _mapper.Map<IEnumerable<AppModel>>(_dbContext.Apps.Where(x => x.UserId == userId && !x.IsDeleted));
        }

        public void Remove(AppModel model)
        {
            _dbContext.Apps.Remove(_mapper.Map<AppEntity>(model));
        }

        public void Update(AppModel model)
        {
            _dbContext.Apps.Update(_mapper.Map<AppEntity>(model));
        }
    }
}
