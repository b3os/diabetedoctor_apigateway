using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureApiService();

builder.WebHost.ConfigureKestrel((options) =>
{
    var certPath = Environment.GetEnvironmentVariable("CERT_PATH");
    var certPassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");
    
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps(certPath, certPassword);
    });
});

var app = builder.Build();

app.ConfigureMiddleware();

app.Run();
