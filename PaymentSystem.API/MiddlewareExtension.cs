using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentSystem.API;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseApiMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        // ADD CORS BEFORE ROUTING!
        app.UseCors("OpenCorsPolicy");
        // app.UseHttpsRedirection(); //HTTP ONLY DEV ENV
        app.UseRouting(); // Important!

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}