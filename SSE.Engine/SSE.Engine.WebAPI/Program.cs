using SSE.Engine.Application.Constants;
using SSE.Engine.Application.Extensions;
using SSE.Engine.WebAPI.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.ConfigureAuth();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.ConfigureConnectionStrings();//Configure Connection Strings

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient(APINames.VECTOR_DATA, httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["APIURLs:VectorData"].ToString());
});

builder.Services.AddServices();

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
