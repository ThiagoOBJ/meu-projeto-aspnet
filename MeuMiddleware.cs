using System.Diagnostics;
using Serilog;

namespace estudoAspNet;

public class TemplateMiddleware
{
    private readonly RequestDelegate _next;

    public TemplateMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Faz algo antes     

        // Chama o próximo middleware na pipeline
        await _next(context);

        // Faz algo depois
    }
}

public class LogTempoMiddleware
{
    private readonly RequestDelegate _next;

    public LogTempoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Faz algo antes     
        var sw = Stopwatch.StartNew();

        // Chama o próximo middleware na pipeline
        await _next(context);

        sw.Stop();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        Log.Information($"Tempo de execução demorou: {sw.Elapsed.TotalMilliseconds} ms ({sw.Elapsed.TotalSeconds} s)");

        // Faz algo depois
    }
}

public static class SerilogExtensions
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog();
    }
}

public static class LogTempoMiddlewareExtensions
{
    public static void UseLogTempp(this WebApplication app)
    {
        app.UseMiddleware<LogTempoMiddleware>();
    }
}