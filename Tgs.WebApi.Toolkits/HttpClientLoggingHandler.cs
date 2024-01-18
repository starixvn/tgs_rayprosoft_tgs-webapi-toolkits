namespace Tgs.WebApi.Toolkits;

internal class HttpClientLoggingHandler<TServiceClient> : DelegatingHandler
{
    private readonly ILogger<TServiceClient> _logger;

    public HttpClientLoggingHandler(ILogger<TServiceClient> logger)
        : base()
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await LogRequestAsync(request, cancellationToken).ConfigureAwait(false);

        HttpResponseMessage response = await base
            .SendAsync(request, cancellationToken)
            .ConfigureAwait(false);
        await LogResponseAsync(response, cancellationToken).ConfigureAwait(false);

        return response;
    }

    private async Task LogRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!_logger.IsEnabled(LogLevel.Information))
            return;

        _logger.LogInformation("Request: {request}", request.ToString());

        if (request.Content != null)
            _logger.LogInformation(
                "Request Body: {content}",
                await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
    }

    private async Task LogResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (!_logger.IsEnabled(LogLevel.Information))
            return;

        _logger.LogInformation("Response: {response}", response.ToString());

        if (response.Content != null)
            _logger.LogInformation(
                "Response Body: {content}",
                await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
    }
}
