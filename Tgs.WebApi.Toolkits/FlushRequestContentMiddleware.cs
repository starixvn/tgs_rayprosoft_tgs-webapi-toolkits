namespace Tgs.WebApi.Toolkits;

internal class FlushRequestContentMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public FlushRequestContentMiddleware(ILoggerFactory loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));

        _logger = loggerFactory.CreateLogger("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware");
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context).ConfigureAwait(false);

        if (_logger.IsEnabled(LogLevel.Information) == true)
        {
            using var stream = new StreamReader(context.Request.Body);

            _ = await stream.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}
