using estudoAspNet;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();

var app = builder.Build();

app.UseLogTempp();

app.UseMiddleware<LogTempoMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
