using Taskly_Api.Request.Authenticate;

namespace Taskly_Api.Response.Table;

public record TableItemResponse
{
    public Guid Id { get; init; }
    public required string Task { get; init; }
    public required string Status { get; init; }
    public required string Label { get; init; }
    public List<UserForTableItemResponse>? Members { get; init; }
    public bool IsCompleted { get; set; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }

}
