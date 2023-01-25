namespace Rubric.Common.Settings.Hangfire;

public record HangfireSettings
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool DisableRecurringJob { get; set; }
}