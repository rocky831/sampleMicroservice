using Microsoft.Extensions.Logging;

namespace AVEVA.PA.MicroserviceTemplate.Infrastructure.Extensions
{
    public static class LoggerExtension
    {
        public static IDisposable BeginScope(this ILogger logger, string key, object value) =>
            logger.BeginScope(new Dictionary<string, object> { { key, value } });

        public static IDisposable BeginScopeWith(this ILogger logger, params (string key, object value)[] keys) =>
            logger.BeginScope(keys.ToDictionary(x => x.key, x => x.value));
    }
}
