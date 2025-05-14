namespace Taskly_Domain.ValueObjects;

public sealed class GeminiSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}