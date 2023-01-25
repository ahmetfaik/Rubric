using System.Text.Json;
using Microsoft.Extensions.Options;
using Rubric.Common.Settings.MassTransit;
using Rubric.Infastructure.Loggers;

namespace Rubric.Common.Settings.Validations;

public class MassTransitSettingsValidation : IValidateOptions<BusSettings>
{
    private readonly IConsoleLogger _logger;
    
    public MassTransitSettingsValidation(IConsoleLogger logger)
    {
        _logger = logger;
    }
    
    public ValidateOptionsResult Validate(string name, BusSettings options)
    {
        _logger.LogTrace($"{nameof(BusSettings)}:{JsonSerializer.Serialize(options)}");

        if (string.IsNullOrEmpty(options.Password))
        {
            _logger.LogError($"{options.GetType().Name}:{nameof(options.Password)} is null");
            return ValidateOptionsResult.Fail($"{options.GetType().Name}:{nameof(options.Password)} is null");
        }

        if (string.IsNullOrEmpty(options.ClusterAddress))
        {
            _logger.LogError($"{options.GetType().Name}:{nameof(options.ClusterAddress)} is null");
            return ValidateOptionsResult.Fail($"{options.GetType().Name}:{nameof(options.ClusterAddress)} is null");
        }

        if (string.IsNullOrEmpty(options.Username))
        {
            _logger.LogError($"{options.GetType().Name}:{nameof(options.Username)} is null");
            return ValidateOptionsResult.Fail($"{options.GetType().Name}:{nameof(options.Username)} is null");
        }


        return ValidateOptionsResult.Success;
    }

}