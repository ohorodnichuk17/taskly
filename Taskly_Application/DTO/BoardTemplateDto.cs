namespace Taskly_Application.DTO;

public record BoardTemplateDto
{
    public required string Name { get; init; }
    public required string ImagePath { get; init; }
}