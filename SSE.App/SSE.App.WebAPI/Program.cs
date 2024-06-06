using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SSE.App.Application.Constants;
using SSE.App.Application.Extensions;
using SSE.App.Domain.Extensions;
using SSE.App.WebAPI.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureAuth();

builder.Services.AddHttpClient(APINames.ENGINE, httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["APIURLs:EngineAPI"].ToString());
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
#if DEBUG
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endif

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRepositories();
builder.Services.AddServices();


builder.Services.AddDbContext<SSE.App.Infrastructure.Data.AppDbContext>(opt => { opt.UseNpgsql(builder.Configuration["ConnectionStrings:Default"]); });

builder.Services.AddRouting(cfg => { cfg.LowercaseUrls = true; });


var app = builder.Build();



#if DEBUG
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endif
app.UseErrorMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
