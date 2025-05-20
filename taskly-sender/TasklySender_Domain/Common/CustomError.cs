using System.Text.Json.Serialization;

namespace TasklySender_Domain.Common;

public class CustomError
{
    [JsonPropertyName("code")]
    public required string Code { get; set; }
    [JsonPropertyName("description")]
    public required string Description { get; set; }
}
