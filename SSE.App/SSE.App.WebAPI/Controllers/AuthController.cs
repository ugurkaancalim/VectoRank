using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SSE.App.Application.Constants;
using SSE.App.Application.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SSE.App.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpGet]
        [Route("generatetoken/{userId:guid}/{appId:guid}")]
        public IActionResult GenerateToken(Guid userId, Guid appId)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            return Ok(_authService.GenerateToken(new Domain.Models.AppModel()
            {
                UserId = userId,
                Id = appId
            }, token));
        }

    }
}
