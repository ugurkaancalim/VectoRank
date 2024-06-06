using SSE.App.Application.Constants;
using SSE.App.Application.Services.Interfaces;
using SSE.App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Services.Implementations
{
    public class EngineService : IEngineService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IAuthService _authService;
        public EngineService(IHttpClientFactory clientFactory,IAuthService authService)
        {
            _clientFactory = clientFactory;
            _authService = authService;
        }

        public async Task<bool> AddApplication(AppModel app)
        {
            var token = _authService.GenerateTokenWithoutUserPermission(app);
            var client = _clientFactory.CreateClient(APINames.ENGINE);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"vector/add/{app.DataId}/{app.LanguageCode}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Convert.ToBoolean(response.Content);
            }
            return false;
        }
    }
}
