using ErrorOr;
using System.Text.Json.Serialization;

namespace TasklySender_Domain.Common;

public class SenderError
{
    [JsonPropertyName("status")]
    public int Status { get; set; }
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    [JsonPropertyName("details")]
    public required string Detail { get; set; }
    [JsonPropertyName("errors")]
    public CustomError[] Errors { get; set; } = [];
}
