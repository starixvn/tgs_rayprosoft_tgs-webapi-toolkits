using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tgs.WebApi.Toolkits.Documentation;

public class SwaggerApiStatusCodeFilter<TTypeConverter> : IOperationFilter where TTypeConverter : class, ITypeConverter
{
    private readonly IServiceProvider _serviceProvider;

    public SwaggerApiStatusCodeFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var typeConverter = ActivatorUtilities.CreateInstance<TTypeConverter>(
            _serviceProvider,
            context.SchemaGenerator,
            context.SchemaRepository);

        var successStatusCodeAttr = context.ApiDescription
            .ActionDescriptor
            .EndpointMetadata
            .OfType<SuccessStatusCodeAttribute>()
            .LastOrDefault();

        foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
        {
            if (!operation.Responses.TryGetValue(responseType.StatusCode.ToString(), out var response))
                continue;

            if (successStatusCodeAttr is not null && responseType.StatusCode == successStatusCodeAttr.StatusCode)
                response.Description = successStatusCodeAttr.Description ?? response.Description;

            response.Content = responseType.ApiResponseFormats.ToDictionary(
                f => f.MediaType,
                f => new OpenApiMediaType
                {
                    Schema = typeConverter.GenerateSuccessSchema(responseType?.Type)
                });
        }

        foreach (var attr in context.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<FailStatusCodeAttribute>())
        {
            operation.Responses.Add(
                attr.StatusCode.ToString(),
                new OpenApiResponse
                {
                    Description = attr.Description,
                    Content = attr.ErrorResponseType == null
                        ? null
                        : new Dictionary<string, OpenApiMediaType>
                        {
                            ["application/json"] = new OpenApiMediaType
                            {
                                Schema = typeConverter.GenerateFailSchema(attr.ErrorResponseType)
                            }
                        }
                });
        }
    }
}
