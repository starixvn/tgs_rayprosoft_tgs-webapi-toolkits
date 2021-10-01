using Swashbuckle.AspNetCore.SwaggerGen;
using Tgs.WebApi.Toolkits;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerOptionsExtensions
    {
        public static void ApplyWebApiToolkitSupport(this SwaggerGenOptions options)
        {
            options.OperationFilter<SwaggerDocFilter>();
        }
    }
}
