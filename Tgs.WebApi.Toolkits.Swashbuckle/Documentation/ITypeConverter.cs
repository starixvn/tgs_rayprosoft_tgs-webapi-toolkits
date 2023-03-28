using Microsoft.OpenApi.Models;

namespace Tgs.WebApi.Toolkits.Documentation;

public interface ITypeConverter
{
    OpenApiSchema? GenerateFailSchema(Type? modelType);

    OpenApiSchema? GenerateSuccessSchema(Type? modelType);
}
