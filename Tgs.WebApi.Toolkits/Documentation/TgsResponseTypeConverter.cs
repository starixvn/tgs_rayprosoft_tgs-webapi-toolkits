using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tgs.WebApi.Toolkits.ViewModels;

namespace Tgs.WebApi.Toolkits.Documentation;

internal class TgsResponseTypeConverter : ITypeConverter
{
    private readonly ISchemaGenerator _schemaGenerator;
    private readonly SchemaRepository _schemaRepository;

    public TgsResponseTypeConverter(ISchemaGenerator schemaGenerator, SchemaRepository schemaRepository)
    {
        _schemaGenerator = schemaGenerator ?? throw new ArgumentNullException(nameof(schemaGenerator));
        _schemaRepository = schemaRepository ?? throw new ArgumentNullException(nameof(schemaRepository));
    }

    public OpenApiSchema? GenerateSuccessSchema(Type? modelType)
    {
        if (modelType == null)
            return null;

        var successViewModelType = typeof(SuccessViewModel<>);

        if (modelType.IsGenericType && modelType.GetGenericTypeDefinition() == successViewModelType)
            return _schemaGenerator.GenerateSchema(modelType, _schemaRepository);

        var wrapType = successViewModelType.MakeGenericType(modelType);

        return _schemaGenerator.GenerateSchema(wrapType, _schemaRepository);
    }

    public OpenApiSchema? GenerateFailSchema(Type? modelType)
        => modelType == null
            ? null
            : _schemaGenerator.GenerateSchema(modelType, _schemaRepository);
}
