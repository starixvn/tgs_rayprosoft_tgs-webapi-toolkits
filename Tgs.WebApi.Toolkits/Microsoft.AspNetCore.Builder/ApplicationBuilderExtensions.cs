using Tgs.WebApi.Toolkits;

namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseFlushRequestContent(this IApplicationBuilder app)
        => app.UseMiddleware<FlushRequestContentMiddleware>();
}
