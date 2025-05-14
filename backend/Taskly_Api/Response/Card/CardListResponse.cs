namespace Taskly_Api.Response.Card;

public record CardListResponse
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public CardResponse[]? Cards { get; init; }
    public Guid BoardId { get; init; }
}
