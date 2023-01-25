using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rubric.Common.Settings.Swagger;

public class SwaggerOptionsConfigure : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerOptionsConfigure(IApiVersionDescriptionProvider provider)
    {
        this._provider = provider;
    }
    
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoApiVersion(description));
        }
    }

    private static OpenApiInfo CreateInfoApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "Api",
            Version = description.ApiVersion.ToString(),
            Description = description.ToString(),
            Contact = new OpenApiContact { Name = "Rubric Team", Email = "info@rubric.com" }
        };

        if (description.IsDeprecated)
        {
            info.Description = "This Api version has been deprecated.";
        }

        return info;
    }
}

