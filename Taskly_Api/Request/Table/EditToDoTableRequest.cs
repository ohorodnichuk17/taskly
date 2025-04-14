namespace Taskly_Api.Request.Table;

public class EditToDoTableRequest
{
    public Guid TableId { get; set; }
    public required string TableName { get; set; }
}