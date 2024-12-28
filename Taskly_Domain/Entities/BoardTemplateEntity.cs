namespace Taskly_Domain.Entities;

public class BoardTemplateEntity // тут будуть фони для дошки
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string ImagePath { get; init; }
}