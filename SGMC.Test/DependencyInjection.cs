﻿using Serilog;

namespace SGMC.Test;

public static class DependencyInjection
{
    public static void AddLogger(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog(logger);
    }
}
