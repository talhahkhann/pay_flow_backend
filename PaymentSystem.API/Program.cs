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

// REMOVE THIS - it's duplicated in AddApiServices
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("OpenCorsPolicy", policy =>
//     {
//         policy.AllowAnyOrigin()
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//     });
// });

var app = builder.Build();

app.UseApiMiddleware();

app.Run();

public partial class Program { }