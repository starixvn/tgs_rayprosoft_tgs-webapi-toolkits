using Tgs.WebApi.Toolkits;

namespace Microsoft.AspNetCore.Mvc;

public static class HttpContextExtensions
{
    public static void TransformResult(this HttpContext httpContext, IActionResult result)
    {
        var responseFeature = new TransformResponseFeature(result);

        httpContext.Features.Set(responseFeature);
    }

    public static TResult TransformResult<TResult>(this HttpContext httpContext, IActionResult result)
    {
        var responseFeature = new TransformResponseFeature(result);

        httpContext.Features.Set(responseFeature);

        return default!;
    }

    public static Task TransformResultAsync(this HttpContext httpContext, IActionResult result)
        => TransformResultValueAsync(httpContext, result).AsTask();

    public static Task<TResult> TransformResultAsync<TResult>(this HttpContext httpContext, IActionResult result)
        => TransformResultValueAsync<TResult>(httpContext, result).AsTask();

    public static ValueTask TransformResultValueAsync(this HttpContext httpContext, IActionResult result)
    {
        TransformResult(httpContext, result);

        return ValueTask.CompletedTask;
    }

    public static ValueTask<TResult> TransformResultValueAsync<TResult>(this HttpContext httpContext, IActionResult result)
    {
        TransformResult(httpContext, result);

        return ValueTask.FromResult<TResult>(default!);
    }
}
