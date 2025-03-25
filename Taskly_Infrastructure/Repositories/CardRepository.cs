using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class CardRepository(TasklyDbContext context) : Repository<CardEntity>(context), ICardRepository
{
    private readonly DbSet<CardEntity> _cards = context.Set<CardEntity>();
    public async Task<Guid?> TransferCardToAnotherCardListAsync(Guid CardListId, Guid CardId)
    {
        var card = await _cards.FirstOrDefaultAsync(card => card.Id == CardId);

        if(card == null)
            return null;

        var isCardListExist = await context.CardLists.AnyAsync(cardList => cardList.Id == CardListId);

        if(!isCardListExist)
            return null;

        card.CardListId = CardListId;

        context.Update(card);
        await context.SaveChangesAsync();

        return card.Id;
    }
}
