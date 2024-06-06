using SSE.App.Application.Services.Interfaces;
using SSE.App.Domain.Models;
using SSE.App.Domain.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Services.Implementations
{
    public class AppService : IAppService
    {
        private readonly IUnitOfWork _uow;
        private readonly IResourceService _resourceService;
        private readonly IAppClaimService _claimService;
        private readonly IEngineService _engineService;
        public AppService(IUnitOfWork uow, IResourceService resourceService, IAppClaimService appClaimService, IEngineService engineService)
        {
            _uow = uow;
            _resourceService = resourceService;
            _claimService = appClaimService;
            _engineService = engineService;
        }
        public async Task Add(AppModel model)
        {
            _claimService.CheckIfUserAppMatch(model.UserId);
            _claimService.CheckIfAppIdMatch(model.Id);
            model.LanguageCode = _resourceService.GetLanguageCode();
            model.CreationDate = DateTime.UtcNow;
            var entity = _uow.AppRepository.Add(model);
            var res = await _engineService.AddApplication(entity);
            if (res)
                _uow.SaveChanges();
            else
                throw new ApplicationException(_resourceService.Get(Constants.ResourceNames.ENGINE_SERVICE_CONNECTION_ERROR));
        }

        public IEnumerable<AppModel> GetAll()
        {
            return _uow.AppRepository.GetByUserId(_claimService.AppClaims.UserId);
        }

        public AppModel? GetById(Guid id)
        {
            _claimService.CheckIfAppIdMatch(id);
            return _uow.AppRepository.GetById(id);
        }

        public void Remove(AppModel model)
        {
            _claimService.CheckIfAppIdMatch(model.Id);
            _uow.AppRepository.Remove(model);
            _uow.SaveChanges();
        }

        public void Update(AppModel model)
        {
            _claimService.CheckIfAppIdMatch(model.Id);
            _uow.AppRepository.Update(model);
            _uow.SaveChanges();
        }
    }
}
