using Serilog;

namespace Taskly_Api.Common;

public static class AppExtensions
{
    public static void SerilogConfiguration(this IHostBuilder host)
    {
        host.UseSerilog((context, loggerConfig) =>
        {
            loggerConfig.WriteTo.Console();
        });
    }
}
