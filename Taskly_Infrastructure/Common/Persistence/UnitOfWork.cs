using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public ITableRepository Table { get; private set; }
    public ITableItemsRepository TableItems { get; private set; }
    public IBoardTemplateRepository BoardTemplates { get; private set; }
    public ICardRepository Cards { get; private set; }
    public IFeedbackRepository Feedbacks { get; private set; }
    public IAchievementRepository Achievements { get; private set; }

    public UnitOfWork(TasklyDbContext context, UserManager<UserEntity> userManager)
    {
        _context = context;
        _userManager = userManager;
        Authentication = new AuthenticationRepository(_userManager, _context);
        Board = new BoardRepository(_context);
        Avatar = new AvatarRepository(_context);
        Table = new TableRepository(_context);
        TableItems = new TableItemsRepository(_context);
        BoardTemplates = new BoardTemplateRepository(_context);
        Cards = new CardRepository(_userManager,_context);
        Feedbacks = new FeedbackRepository(_context);
        Achievements = new AchievementRepository(_context);
    }
    
    public async Task SaveChangesAsync(string errorMessage)
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException(errorMessage, ex);
        }
    }
}