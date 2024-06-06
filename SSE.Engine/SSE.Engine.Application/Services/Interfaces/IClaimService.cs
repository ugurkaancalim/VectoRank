using SSE.Engine.Application.DataStructures.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Application.Services.Interfaces
{
    public interface IClaimService
    {
        AppClaimInfo AppClaims { get; }
        void CheckIfUserAppMatch(Guid? userId);
        void CheckIfAppIdMatch(Guid? appId);
    }
}
