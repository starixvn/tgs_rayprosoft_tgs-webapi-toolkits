using Swashbuckle.AspNetCore.SwaggerGen;
using Tgs.WebApi.Toolkits.Documentation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerOptionsExtensions
    {
        public static void ApplyWebApiToolkitSupport(this SwaggerGenOptions options)
            => options.OperationFilter<SwaggerApiStatusCodeFilter<NormalTypeConverter>>();

        public static void ApplyTgsWebApiSupport(this SwaggerGenOptions options)
            => options.OperationFilter<SwaggerApiStatusCodeFilter<TgsResponseTypeConverter>>();
    }
}
