using gRPC.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5031, o => o.Protocols =
        HttpProtocols.Http2);
});

var app = builder.Build();

app.MapGrpcService<InsuranceService>();

app.Run();

