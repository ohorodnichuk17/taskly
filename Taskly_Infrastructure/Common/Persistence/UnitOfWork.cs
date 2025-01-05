using Microsoft.AspNetCore.Identity;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Repositories;

namespace Taskly_Infrastructure.Common.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private TasklyDbContext _context;
    private UserManager<UserEntity> _userManager;
    public IAuthenticationRepository Authentication { get; private set; }
    public IBoardRepository Board { get; private set; }
    public IAvatarRepository Avatar { get; private set; }
    public IToDoItemRepository ToDoItem { get; private set; }
    public IToDoTableRepository ToDoTable { get; private set; }

    public UnitOfWork(TasklyDbContext context, UserManager<UserEntity> userManager)
    {
        _context = context;
        _userManager = userManager;
        Authentication = new AuthenticationRepository(_userManager, _context);
        Board = new BoardRepository(_context);
        Avatar = new AvatarRepository(_context);
        ToDoItem = new ToDoItemRepository(_context);
        ToDoTable = new ToDoTableRepository(_context);
    }
}