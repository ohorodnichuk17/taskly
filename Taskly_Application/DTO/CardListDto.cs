namespace Taskly_Application.DTO;

public class CardListDto
{
    public required string Title { get; set; }
    public ICollection<CardDto> Cards { get; set; } = new List<CardDto>();
}