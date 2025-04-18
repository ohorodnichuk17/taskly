using Taskly_Api.Request.Authenticate;

namespace Taskly_Api.Response.Table;

public class TableResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<TableItemResponse> Items { get; set; }
    public List<UserForTableItemResponse>? Members { get; init; }
}