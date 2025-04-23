namespace Taskly_Api.Request.Table;

public record EditTableRequest
{
    public Guid TableId { get; set; }
    public required string TableName { get; set; }
}