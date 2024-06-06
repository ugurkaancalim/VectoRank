using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSE.App.Application.Services.Interfaces;
using SSE.App.Domain.Models;

namespace SSE.App.WebAPI.Controllers
{
    [Authorize]
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
        public IActionResult GetAll()
        {
            return Ok(_appService.GetAll());
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById(Guid appId)
        {
            return Ok(_appService.GetById(appId));
        }
        [HttpPost]
        public async Task<IActionResult> Add(AppModel model)
        {
            await _appService.Add(model);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(AppModel model)
        {
            _appService.Update(model);
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(AppModel model)
        {
            _appService.Remove(model);
            return Ok();
        }

    }
}
