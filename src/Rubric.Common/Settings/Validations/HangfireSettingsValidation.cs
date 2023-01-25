using System.Text.Json;
using Microsoft.Extensions.Options;
using Rubric.Common.Settings.Hangfire;
using Rubric.Infastructure.Loggers;

namespace Rubric.Common.Settings.Validations;

public class HangfireSettingsValidation : IValidateOptions<HangfireSettings>
{
    private readonly IConsoleLogger _logger;

    public HangfireSettingsValidation(IConsoleLogger consoleLogger)
    {
        _logger = consoleLogger;
    }
    
    public ValidateOptionsResult Validate(string name, HangfireSettings options)
    {
        _logger.LogTrace($"{nameof(HangfireSettings)}:{JsonSerializer.Serialize(options)}");

        if (string.IsNullOrEmpty(options.Username))
        {
            _logger.LogError($"{options.GetType().Name}:{nameof(options.Username)} is not set");
            return ValidateOptionsResult.Fail(
                $"{options.GetType().Name}:{nameof(options.Username)} is not set");
        }

        if (string.IsNullOrEmpty(options.Password))
        {
            _logger.LogError($"{options.GetType().Name}:{nameof(options.Password)} is not set");
            return ValidateOptionsResult.Fail(
                $"{options.GetType().Name}:{nameof(options.Password)} is not set");
        }

        return ValidateOptionsResult.Success;
    }
}