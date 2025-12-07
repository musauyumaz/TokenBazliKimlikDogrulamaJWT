using AuthServer.API.Configurations.Extensions;
using AuthServer.Application;
using AuthServer.Infrastructure;
using AuthServer.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

app.ConfigureMiddleware();

app.Run();