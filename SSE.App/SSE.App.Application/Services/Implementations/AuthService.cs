using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SSE.App.Application.Constants;
using SSE.App.Application.ExceptionHandling.ExceptionTypes;
using SSE.App.Application.Services.Interfaces;
using SSE.App.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IResourceService _resourceService;
        public AuthService(IConfiguration configuration, IResourceService resourceService)
        {
            _configuration = configuration;
            _resourceService = resourceService;
        }
        public string GenerateToken(AppModel appModel, string userToken)
        {
            CheckUserTokenValidity(userToken, appModel.UserId);
            return GenerateTokenWithoutUserPermission(appModel);
        }

        public string GenerateTokenWithoutUserPermission(AppModel appModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimConstants.AppId, appModel.Id.ToString()),
                    new Claim(ClaimConstants.UserId,appModel.UserId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["UserAPIJwtSettings:Issuer"].ToString(),
                ValidAudience = _configuration["UserAPIJwtSettings:Audience"].ToString(),
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["UserAPIJwtSettings:Secret"].ToString())),
                ClockSkew = TimeSpan.Zero // Adjust the tolerance for the expiration time
            };
        }
        private void CheckUserTokenValidity(string token, Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            try
            {
                // Validate token
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var guid = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (guid == userId)
                    throw new AppException(_resourceService.Get(ResourceNames.AUTHORIZATION_ERROR));
            }
            catch (Exception ex)
            {
                throw new AppException(_resourceService.Get(ResourceNames.AUTHORIZATION_ERROR));
            }
        }
    }
}
