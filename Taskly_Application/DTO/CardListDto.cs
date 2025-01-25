namespace Taskly_Application.DTO;

public record CardListDto
{
    public required string Title { get; set; }
    public ICollection<CardDto> Cards { get; set; } = new List<CardDto>();
}