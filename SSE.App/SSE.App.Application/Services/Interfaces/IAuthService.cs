using SSE.App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(AppModel appModel,string userToken);
        string GenerateTokenWithoutUserPermission(AppModel appModel);
    }
}
