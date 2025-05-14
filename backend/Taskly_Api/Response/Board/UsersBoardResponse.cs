namespace Taskly_Api.Response.Board;

public record UsersBoardResponse
{
    public Guid Id { get; set; }
    public required string  Name { get; set; }
    public int CountOfMembers { get; set; }
    public required string BoardTemplateName { get; set; }
    public required string BoardTemplateColor { get; set; }
}
