using SSE.App.Application.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Services.Interfaces
{
    public interface IAppClaimService
    {
        AppClaimInfo AppClaims { get; }
        void CheckIfUserAppMatch(Guid? userId);
        void CheckIfAppIdMatch(Guid? appId);
    }
}
