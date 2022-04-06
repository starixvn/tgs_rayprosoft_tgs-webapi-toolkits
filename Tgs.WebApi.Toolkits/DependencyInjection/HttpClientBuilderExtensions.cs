using Microsoft.Extensions.DependencyInjection.Extensions;
using Tgs.WebApi.Toolkits;

namespace Microsoft.Extensions.DependencyInjection;

public static class HttpClientBuilderExtensions
{
    public static IServiceCollection AddHttpClientLoggingHandler(this IServiceCollection services)
    {
        services.TryAddTransient(typeof(HttpClientLoggingHandler<>));

        return services;
    }

    public static IHttpClientBuilder AddLoggingHandler<TServiceClient>(this IHttpClientBuilder httpClientBuilder)
    {
        httpClientBuilder.Services.AddHttpClientLoggingHandler();

        return httpClientBuilder.AddHttpMessageHandler<HttpClientLoggingHandler<TServiceClient>>();
    }

    public static DelegatingHandler ResolveLoggingHandler<TServiceClient>(this IServiceProvider serviceProvider)
        => serviceProvider.GetRequiredService<HttpClientLoggingHandler<TServiceClient>>();
}
