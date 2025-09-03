using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 52428800; // 50 MB
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 52428800; // 50 MB
});

builder.ConfigureApiService();


// builder.WebHost.ConfigureKestrel((options) =>
// {
//     var certPath = Environment.GetEnvironmentVariable("CERT__PATH");
//     var certPassword = Environment.GetEnvironmentVariable("CERT__PASSWORD");

//     options.ListenAnyIP(5001, listenOptions =>
//     {
//         listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
//         listenOptions.UseHttps(certPath, certPassword);
//     });
// });

var app = builder.Build();

app.ConfigureMiddleware();

app.Run();
