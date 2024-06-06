using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSE.Engine.Application.Services.Interfaces;

namespace SSE.Engine.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService _queryService;
        public QueryController(IQueryService queryService)
        {
            _queryService = queryService;
        }
        [HttpGet]
        [Route("execute")]
        public async Task<IActionResult> Execute(string query)
        {
            return Ok(await _queryService.Execute(query));
        }
    }
}
