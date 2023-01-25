using System.Reflection;
using System.Text.Json;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Rubric.Scheduler.Api.Filters;

namespace Rubric.Scheduler.Api.Installers;

public static class ControllerInstaller
{
    public static void InstallController(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers(s =>
            {
                s.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions()));
                s.ReturnHttpNotAcceptable = true;
                s.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status202Accepted));
                s.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                s.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                s.Filters.Add<ValidationFilter>();
            })
            .AddFluentValidation(t => t.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

        serviceCollection.Configure<ApiBehaviorOptions>(s => { s.SuppressModelStateInvalidFilter = true; });
    }
}