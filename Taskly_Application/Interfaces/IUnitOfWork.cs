using Taskly_Application.Interfaces.IRepository;

namespace Taskly_Application.Interfaces;

public interface IUnitOfWork
{ 
    IAuthenticationRepository Authentication { get; }
    IBoardRepository Board { get; }
    IAvatarRepository Avatar { get; }
    IToDoItemRepository ToDoItem { get; }
    IToDoTableRepository ToDoTable { get; }
}