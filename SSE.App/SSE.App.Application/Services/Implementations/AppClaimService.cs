using Microsoft.AspNetCore.Http;
using SSE.App.Application.Constants;
using SSE.App.Application.DataStructures;
using SSE.App.Application.ExceptionHandling.ExceptionTypes;
using SSE.App.Application.Services.Interfaces;
using SSE.App.Domain.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Services.Implementations
{
    public class AppClaimService : IAppClaimService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Interfaces.IResourceService _resourceService;
        public AppClaimService(IHttpContextAccessor contextAccessor, Interfaces.IResourceService resourceService)
        {
            _contextAccessor = contextAccessor;
            _resourceService = resourceService;
        }
        private AppClaimInfo? _appClaimInfo;
        public AppClaimInfo AppClaims
        {
            get
            {
                if (_appClaimInfo == null)
                {
                    var user = _contextAccessor.HttpContext.User;
                    _appClaimInfo = new AppClaimInfo()
                    {
                        UserId = Guid.Parse(user?.FindFirst(ClaimConstants.UserId)?.Value),
                        AppId = Guid.Parse(user?.FindFirst(ClaimConstants.AppId)?.Value)
                    };
                }
                return _appClaimInfo;
            }
        }

        public void CheckIfUserAppMatch(Guid? userId)
        {
            if (AppClaims.UserId != userId)
                throw new AppException(_resourceService.Get(Constants.ResourceNames.AUTHORIZATION_ERROR));
        }
        public void CheckIfAppIdMatch(Guid? appId)
        {
            if (AppClaims.AppId != appId)
                throw new AppException(_resourceService.Get(Constants.ResourceNames.AUTHORIZATION_ERROR));
        }
    }
}
