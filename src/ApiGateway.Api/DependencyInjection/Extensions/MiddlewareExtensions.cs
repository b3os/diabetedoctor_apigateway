namespace ApiGateway.Api.DependencyInjection.Extensions;

public static class MiddlewareExtensions
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) { }

        app.UseAuthentication();
        app.UseAuthorization();
        
        // Middleware gắn header X-User-Id, X-User-Roles vào request
        app.Use(CustomMiddlewares.AttachUserHeaderMiddleware);

        app.MapReverseProxy();
    }
}
