using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Options;
using Rubric.Common.Settings.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rubric.Scheduler.Api.Installers;

public static class SwaggerInstaller
{
    public static void InstallSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VV";
            options.SubstituteApiVersionInUrl = true;
        });

        serviceCollection.AddApiVersioning(options =>
        {
            options.Conventions.Add(new VersionByNamespaceConvention());
            options.DefaultApiVersion = new ApiVersion(0, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        serviceCollection.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfigure>();

        serviceCollection.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            options.EnableAnnotations();
            options.OperationFilter<SwaggerDefaultValuesConfigure>();

            options.CustomSchemaIds(x => x.ToString());
        });
    }
}