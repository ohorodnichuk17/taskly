using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class CardCommentRepository(TasklyDbContext context) : Repository<CardCommentEntity>(context), ICardCommentRepository
{
    private readonly DbSet<CardCommentEntity> commentEntitie = context.Set<CardCommentEntity>();
    public async Task<Guid?> LeaveCommentAsync(Guid CardId, Guid UserId, string Text)
    {
        try
        {
            var newComment = new CardCommentEntity()
            {
                Id = Guid.NewGuid(),
                Text = Text,
                CardId = CardId,
                UserId = UserId,
                CreatedAt = DateTime.Now,
            };

            await CreateAsync(newComment);
            return newComment.Id;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<CardCommentEntity[]?> GetCommentsByCardIdAsync(Guid CardId)
    {
        var card = await context.Cards.FirstOrDefaultAsync();

        if(card == null)
            return null;

        return await commentEntitie
            .Where(u => u.CardId == CardId)
            .Include(c => c.User)
            .ThenInclude(u => u!.Avatar)
            .ToArrayAsync();
    }
}
