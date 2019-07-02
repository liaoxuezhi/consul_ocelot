using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using SerilogCore.Models;
using System;
using System.IO;

namespace SerilogCore
{
    public static class SerilogBuilder
    {
        public static IApplicationBuilder AddSerilog(this IApplicationBuilder app, ILoggerFactory loggerFactory, SerilogService serilogService)
        {
            var path = "log-{Date}.txt";
            if (serilogService != null)
            {
                if (!string.IsNullOrEmpty(serilogService.FilePath))
                {
                    path = Path.Combine(serilogService.FilePath, path);
                }
            }
            var serilog = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.RollingFile(path)
            .WriteTo.Console();
            loggerFactory.AddSerilog(serilog.CreateLogger());
            return app;
        }
    }
}
