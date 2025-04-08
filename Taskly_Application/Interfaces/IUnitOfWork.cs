using Taskly_Application.Interfaces.IRepository;

namespace Taskly_Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync(string errorMessage);
    IAuthenticationRepository Authentication { get; }
    IBoardRepository Board { get; }
    IAvatarRepository Avatar { get; }
    IToDoTableRepository ToDoTable { get; }
    IToDoTableItemsRepository ToDoTableItems { get; }
    IBoardTemplateRepository BoardTemplates { get; }
    ICardRepository Cards { get; }
}