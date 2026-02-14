using PaymentSystem.API;
using PaymentSystem.Application;
using PaymentSystem.Persistence;
using PaymentSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApiServices(builder.Configuration);  // CORS is already registered here

var app = builder.Build();
app.UseRateLimiter();
app.UseApiMiddleware();

app.Run();

public partial class Program { }