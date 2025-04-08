var builder = WebApplication.CreateBuilder(args);

// Add authentication first

#region Authen
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = "Bearer";
//    options.DefaultChallengeScheme = "Bearer";
//})
//.AddJwtBearer("Bearer", options =>
//{
//    options.Authority = "https://localhost:1121";
//    options.Audience = "DiabletAuthService";
//    options.RequireHttpsMetadata = false;

//    // Configure token validation
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ClockSkew = TimeSpan.Zero, // Remove clock skew for testing
//        ValidIssuer = "https://localhost:1121",
//        ValidAudience = "DiabletAuthService"
//    };

//    // Configure metadata
//    options.MetadataAddress = "https://localhost:1121/.well-known/openid-configuration";

//    // Add event handlers for better debugging
//    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
//    {
//        OnAuthenticationFailed = context =>
//        {
//            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
//            return Task.CompletedTask;
//        },
//        OnTokenValidated = context =>
//        {
//            Console.WriteLine("Token validated successfully");
//            return Task.CompletedTask;
//        },
//        OnChallenge = context =>
//        {
//            Console.WriteLine($"Challenge: {context.Error}");
//            return Task.CompletedTask;
//        }
//    };
//});

//// Add authorization policies
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("userPolicy", policy => policy
//        .RequireClaim(ClaimTypes.Role, "1")
//        .RequireAuthenticatedUser());
//});

#endregion

// Add reverse proxy after authentication
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure authentication and authorization are before the reverse proxy
app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();

app.MapReverseProxy();

app.Run();
