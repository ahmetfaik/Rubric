using System.Text.Json;
using Microsoft.Extensions.Options;
using Rubric.Common.Settings.Postgres;
using Rubric.Infastructure.Loggers;

namespace Rubric.Common.Settings.Validations;

public class PostgresSettingsValidation : IValidateOptions<PostgresSettings>
{
    private readonly IConsoleLogger _logger;

    public PostgresSettingsValidation(IConsoleLogger logger)
    {
        _logger = logger;
    }

    public ValidateOptionsResult Validate(string name, PostgresSettings options)
    {
        _logger.LogTrace($"{nameof(PostgresSettings)}:{JsonSerializer.Serialize(options)}");

        if (string.IsNullOrEmpty(options.ConnectionString))
        {
            _logger.LogError($"{options.GetType().Name}:{nameof(options.ConnectionString)} is not set");
            return ValidateOptionsResult.Fail(
                $"{options.GetType().Name}:{nameof(options.ConnectionString)} is not set");
        }

        return ValidateOptionsResult.Success;
    }
}