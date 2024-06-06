using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSE.Engine.Application.Services.Interfaces;

namespace SSE.Engine.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {

        private readonly IAppService _appService;
        public AppController(IAppService appService)
        {
            _appService = appService;
        }
        [HttpGet]
        [Route("add/{dataId:long}/{languageCode}")]
        public async Task<IActionResult> AddApplication(long dataId,string languageCode)
        {
           await _appService.Add(dataId, languageCode);
            return Ok();
        }
    }
}
