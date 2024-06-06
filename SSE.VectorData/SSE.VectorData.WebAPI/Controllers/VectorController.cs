using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSE.VectorData.Application.Services.Interfaces;
using System.Diagnostics;

namespace SSE.VectorData.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VectorController : ControllerBase
    {
        private readonly IVectorService _vectorService;
        private readonly IConfiguration _configuration;
        public VectorController(IVectorService vectorService,IConfiguration configuration)
        {
            _vectorService = vectorService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult Get(string Word)
        {
            return Ok(_vectorService.GetVector(Word));
        }

        [HttpPost]
        [Route("getlist")]
        public IActionResult GetVectors(List<string> words)
        {
            return Ok(_vectorService.GetVectors(words));
        }

        [HttpGet]
        [Route("languages")]
        public IActionResult GetLanguages()
        {
            return Ok(_configuration["Language"]);
        }

        [HttpGet]
        [Route("InsertVectors")]
        public IActionResult InsertVectors()
        {
            _vectorService.InsertBaseVectors(@"C:\Users\ranna\Downloads\Downloader\Data\Fasttext\cc.tr.300.vec\cc.tr.300.vec");
            return Ok();
        }
    }
}
