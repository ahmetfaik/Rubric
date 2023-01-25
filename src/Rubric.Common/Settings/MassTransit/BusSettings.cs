namespace Rubric.Common.Settings.MassTransit;

public record BusSettings
{
    public string ClusterAddress { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}