namespace ApiGateway.Api.DependencyInjection.Extensions;

/// <summary>
/// Extension methods để cấu hình các Services cho API Gateway,
/// bao gồm cấu hình Authentication, Authorization và Reverse Proxy.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Đăng ký cấu hình từ env
    /// </summary>
    private static IServiceCollection AddAuthSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthSettings>(configuration.GetSection(AuthSettings.SectionName));
        return services;
    }

    /// <summary>
    /// Đăng ký Authentication theo chuẩn JWT Bearer và Authorization.
    /// </summary>
    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Lấy cấu hình AuthSettings
        var authSettings = configuration.GetSection(AuthSettings.SectionName).Get<AuthSettings>();

        if (authSettings == null)
            throw new Exception("Please add AuthSettings to the configuration.");

        // Đăng ký Authentication với JWT Bearer
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authSettings.Issuer,
                ValidAudience = authSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.AccessSecretToken))
            };
        });

        services.AddAuthorization();

        return services;
    }

    /// <summary>
    /// Đóng gói các method
    /// </summary>
    public static void ConfigureApiService(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddAuthSettingsConfiguration(builder.Configuration)
            .AddJwtAuthentication(builder.Configuration);

        builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection(ApiGatewayConstants.ReverseProxy));
    }
}
