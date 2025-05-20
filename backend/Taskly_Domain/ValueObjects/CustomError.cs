using System.Text.Json.Serialization;

namespace Taskly_Domain.ValueObjects;

public class CustomError
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
