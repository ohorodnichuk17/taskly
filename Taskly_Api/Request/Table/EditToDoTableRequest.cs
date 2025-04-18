namespace Taskly_Api.Request.Table;

public class EditTableRequest
{
    public Guid TableId { get; set; }
    public required string TableName { get; set; }
}