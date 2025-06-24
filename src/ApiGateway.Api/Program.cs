var builder = WebApplication.CreateBuilder(args);

builder.ConfigureApiService();

var app = builder.Build();

app.ConfigureMiddleware();

app.Run();
