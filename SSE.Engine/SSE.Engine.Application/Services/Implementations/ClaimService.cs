using Microsoft.AspNetCore.Http;
using SSE.Engine.Application.Constants;
using SSE.Engine.Application.DataStructures.App;
using SSE.Engine.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Application.Services.Implementations
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ClaimService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            
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
                throw new Exception("Authorization Error");
        }
        public void CheckIfAppIdMatch(Guid? appId)
        {
            if (AppClaims.AppId != appId)
                throw new Exception("Authorization Error");
        }
    }
}
