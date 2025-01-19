using System.Text.Json.Serialization;

namespace Taskly_Application.DTO;

public class TemplateBoardDto
{
    public required string Name { get; set; }
    public string Tag { get; init; }
    public bool IsTeamBoard { get; set; }
    public ICollection<CardListDto> CardLists { get; set; } = new List<CardListDto>();
    public ICollection<MemberDto> Members { get; set; } = new List<MemberDto>();
    public ICollection<BoardTemplateDto> BoardTemplates { get; set; } = new List<BoardTemplateDto>();
}