using SSE.VectorData.Domain.Extensions;
using SSE.VectorData.Application.Extensions;
using SSE.VectorData.Infrastructure.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
//Inject Repositories
builder.Services.AddRepositories();
//Inject Services
builder.Services.AddServices();

builder.Services.AddSingleton<VDDbContext>(x => new VDDbContext(builder.Configuration["ConnectionString:Default"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
