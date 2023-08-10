using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tgs.WebApi.Toolkits;

public class TransformResponseFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
            return;

        var successStatusCodeAttr = context.ActionDescriptor.EndpointMetadata.OfType<SuccessStatusCodeAttribute>().LastOrDefault();
        var transformResponseFeature = context.HttpContext.Features.Get<TransformResponseFeature>();
        var responseFeature = context.HttpContext.Features.Get<IHttpResponseFeature>();

        if (responseFeature is null)
            _ = await next();

        if (transformResponseFeature != null)
            context.Result = transformResponseFeature.ActionResult;
        else if (successStatusCodeAttr != null)
            responseFeature!.StatusCode = successStatusCodeAttr.StatusCode;

        _ = await next();
    }
}
