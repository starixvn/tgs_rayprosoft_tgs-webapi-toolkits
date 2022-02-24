using Microsoft.Extensions.DependencyInjection.Extensions;
using Tgs.WebApi.Toolkits;

namespace Microsoft.Extensions.DependencyInjection;

public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder AddLoggingHandler<TServiceClient>(this IHttpClientBuilder httpClientBuilder)
    {
        httpClientBuilder.Services.TryAddTransient(typeof(HttpClientLoggingHandler<>));

        return httpClientBuilder.AddHttpMessageHandler<HttpClientLoggingHandler<TServiceClient>>();
    }
}
