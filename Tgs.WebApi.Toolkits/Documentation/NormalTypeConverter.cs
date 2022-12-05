using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tgs.WebApi.Toolkits.Documentation;

internal class NormalTypeConverter : ITypeConverter
{
    private readonly ISchemaGenerator _schemaGenerator;
    private readonly SchemaRepository _schemaRepository;

    public NormalTypeConverter(ISchemaGenerator schemaGenerator, SchemaRepository schemaRepository)
    {
        _schemaGenerator = schemaGenerator ?? throw new ArgumentNullException(nameof(schemaGenerator));
        _schemaRepository = schemaRepository ?? throw new ArgumentNullException(nameof(schemaRepository));
    }

    public OpenApiSchema? GenerateSuccessSchema(Type? modelType)
        => modelType == null
            ? null
            : _schemaGenerator.GenerateSchema(modelType, _schemaRepository);
    public OpenApiSchema? GenerateFailSchema(Type? modelType) 
        => modelType == null
            ? null
            : _schemaGenerator.GenerateSchema(modelType, _schemaRepository);
}
