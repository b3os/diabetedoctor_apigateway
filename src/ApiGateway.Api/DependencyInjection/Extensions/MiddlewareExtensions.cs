namespace ApiGateway.Api.DependencyInjection.Extensions;

public static class MiddlewareExtensions
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) { }

        app.UseCors("AllowAll");
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        // app.UseHttpsRedirection();
        // Middleware gắn header X-User-Id, X-User-Roles vào request
        app.Use(CustomMiddlewares.AttachUserHeaderMiddleware);

        app.MapReverseProxy();
        
        app.MapMethods("{**any}", new[] { "OPTIONS" }, context =>
        {
            context.Response.StatusCode = StatusCodes.Status204NoContent;
            return Task.CompletedTask;
        });
    }
}
