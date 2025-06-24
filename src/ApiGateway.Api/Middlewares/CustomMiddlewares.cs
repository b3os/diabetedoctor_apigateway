namespace ApiGateway.Api.Middlewares;

public static class CustomMiddlewares
{
    /// <summary>
    /// Middleware dùng để gán thông tin user dưới dạng HTTP header:
    /// - X-User-Id: userId từ JWT
    /// - X-User-Roles: role từ JWT
    /// - Ghi log ra console với userId, role và đường dẫn API được gọi.
    /// </summary>
    public static RequestDelegate AttachUserHeaderMiddleware(RequestDelegate next) => async context =>
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            // Lấy thông tin từ claim
            var userId = context.User.FindFirst(AuthConstants.UserId)?.Value;
            var roles = context.User.FindAll(AuthConstants.Role).Select(r => r.Value);
            var roleString = string.Join(",", roles);

            // Gắn userId vào header
            if (!string.IsNullOrEmpty(userId))
            {
                context.Request.Headers[ApiGatewayConstants.HeaderUserId] = userId;
            }

            // Gắn role vào header
            if (!string.IsNullOrEmpty(roleString))
            {
                context.Request.Headers[ApiGatewayConstants.HeaderUserRoles] = roleString;
            }

            Console.WriteLine($"[User {userId}] called API {context.Request.Path}, Roles: {roleString}");
        }
        else
        {
            Console.WriteLine($"[Guest] called API {context.Request.Path}");
        }

        await next(context);
    };
}