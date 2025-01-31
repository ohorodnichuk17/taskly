namespace Taskly_Application.DTO.TemplateBoardDTOs;

public record BoardTemplateDto
{
    public required string Name { get; init; }
    public required string ImagePath { get; init; }
}