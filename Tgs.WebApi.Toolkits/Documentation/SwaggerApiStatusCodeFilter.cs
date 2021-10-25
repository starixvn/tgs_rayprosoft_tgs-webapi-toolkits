using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tgs.WebApi.Toolkits.Documentation
{
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

            var successStatusCodeAttr = context
                .ApiDescription
                .ActionDescriptor
                .EndpointMetadata
                .OfType<SuccessStatusCodeAttribute>()
                .LastOrDefault();

            if (successStatusCodeAttr != null)
            {
                operation.Responses.Clear();

                var responseType = context.ApiDescription
                    .SupportedResponseTypes
                    .FirstOrDefault();

                operation.Responses.Add(
                    successStatusCodeAttr.StatusCode.ToString(),
                    new OpenApiResponse
                    {
                        Description = successStatusCodeAttr.Description,
                        Content = responseType == null || responseType.Type == typeof(void)
                            ? null
                            : new Dictionary<string, OpenApiMediaType>
                            {
                                ["application/json"] = new OpenApiMediaType
                                {
                                    Schema = typeConverter.GenerateSuccessSchema(responseType.Type)
                                }
                            }
                    });
            }
            else
            {
                foreach (var response in operation.Responses)
                {
                    var resType = context.ApiDescription.SupportedResponseTypes
                        .Where(r => r.StatusCode.ToString() == response.Key)
                        .FirstOrDefault();

                    foreach (var content in response.Value.Content)
                        content.Value.Schema = typeConverter.GenerateSuccessSchema(resType?.Type);
                }
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
}
