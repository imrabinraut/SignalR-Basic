using SignalRServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed(_ => true)
              .AllowCredentials();
    });
});

builder.Services.AddSignalR();
var app = builder.Build();

app.UseCors();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHubs>("/chathub");
});

app.Run();