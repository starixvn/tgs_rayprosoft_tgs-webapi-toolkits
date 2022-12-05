using System.ComponentModel;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tgs.WebApi.Toolkits.Documentation;

namespace Microsoft.Extensions.DependencyInjection;

public static class SwaggerOptionsExtensions
{
    public static void ApplyDisplayNameSchemaId(this SwaggerGenOptions options)
        => options.CustomSchemaIds(DisplayNameSchemaIdSelector);

    public static void ApplyTgsWebApiSupport(this SwaggerGenOptions options)
        => options.OperationFilter<SwaggerApiStatusCodeFilter<TgsResponseTypeConverter>>();

    public static void ApplyWebApiToolkitSupport(this SwaggerGenOptions options)
                        => options.OperationFilter<SwaggerApiStatusCodeFilter<NormalTypeConverter>>();

    private static string DisplayNameSchemaIdSelector(Type modelType)
    {
        var name = modelType.Name;

        var displayNameAttr = modelType.GetCustomAttribute<DisplayNameAttribute>();
        if (displayNameAttr is not null)
        {
            name = $"{displayNameAttr.DisplayName}{(modelType.IsArray ? "Array" : string.Empty)}";
        }

        return !modelType.IsConstructedGenericType
            ? name.Replace("[]", "Array")
            : modelType.GetGenericArguments()
            .Select(genericArg => DisplayNameSchemaIdSelector(genericArg))
            .Aggregate((string previous, string current) => $"{previous}_{current}") + "Of" + name.Split('`').First();
    }
}
