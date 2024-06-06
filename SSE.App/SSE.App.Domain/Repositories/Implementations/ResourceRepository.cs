using AutoMapper;
using SSE.App.Domain.Models;
using SSE.App.Domain.Repositories.Interfaces;
using SSE.App.Infrastructure.Data;
using SSE.App.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Domain.Repositories.Implementations
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ResourceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = Mapping.MapperCreator.Mapper;
        }
        public void Add(ResourceModel resource)
        {
            _dbContext.Resources.Add(_mapper.Map<ResourceEntity>(resource));
        }

        public string? Get(string resourceName, string languageCode)
        {
            return _dbContext.Resources.FirstOrDefault(x => x.Name == resourceName && x.LanguageCode == languageCode)?.Value;
        }
    }
}
