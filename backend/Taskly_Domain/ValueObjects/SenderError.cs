

using System.Text.Json.Serialization;

namespace Taskly_Domain.ValueObjects;

public class SenderError
{
    [JsonPropertyName("status")]
    public int Status { get; set; }
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("type")]
    public  string? Type { get; set; }
    [JsonPropertyName("detail")]
    public  string? Detail { get; set; }
    [JsonPropertyName("errors")]
    public CustomError[] Errors { get; set; } = [];
}
