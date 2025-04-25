using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain;
using Taskly_Domain.Entities;
using Taskly_Domain.Other;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class CardRepository(UserManager<UserEntity> userManager, TasklyDbContext context) : Repository<CardEntity>(context), ICardRepository
{
    private readonly DbSet<CardEntity> _cards = context.Set<CardEntity>();
    public async Task<Guid?> TransferCardToAnotherCardListAsync(Guid CardListId, Guid CardId)
    {
        var card = await _cards.FirstOrDefaultAsync(card => card.Id == CardId);

        if(card == null)
            return null;

        var cardList = await context.CardLists.FirstOrDefaultAsync(cardList => cardList.Id == CardListId);

        if(cardList == null)
            return null;

        card.CardListId = CardListId;
        card.Status = cardList.Title;
        card.IsCompleated = cardList.Title == Constants.Done;
       

        context.Update(card);
        await context.SaveChangesAsync();

        return card.Id;
    }
    public async Task RemoveCardFromBoardAsync(Guid CardId)
    {
        var card = await _cards.FirstOrDefaultAsync(card => card.Id == CardId);

        if (card != null)
        {
            _cards.Remove(card);
            await context.SaveChangesAsync();
        }
    }
    public async Task<Guid?> ChangeCardAsync(Guid CardId, ChangeCardProps ChangeCardProps)
    {
        var card = await GetByIdAsync(CardId);
        if (card == null)
            return null;

        card.Description = ChangeCardProps.Description != null ? ChangeCardProps.Description : card.Description;

        
        if(ChangeCardProps.Deadline != null)
        {
            var timeRange = await context.TimeRanges.FirstOrDefaultAsync(t => t.Id == card.TimeRangeEntityId);
            if (timeRange != null)
            {
                timeRange.EndTime = ChangeCardProps.Deadline.Value;
                context.TimeRanges.Update(timeRange);
            }
        }

        await SaveAsync(card);

        return card.Id;
    }

    public async Task<Guid?> LeaveCardAsync(Guid CardId)
    {
        var card = await GetByIdAsync(CardId);

        if (card == null)
            return null;

        card.UserId = null;

        await SaveAsync(card);

        return card.Id;
    }

    public async Task<Guid?> TakeCardAsync(Guid CardId, Guid UserId)
    {
        var card = await GetByIdAsync(CardId);

        if (card == null)
            return null;

        var user = await userManager.FindByIdAsync(UserId.ToString());

        if (user == null)
            return null;

        card.UserId = user.Id;

        await SaveAsync(card);

        return card.Id;
    }

    public async Task<Guid?> CreateCardAsync(Guid CardListId, string Task, DateTime Deadline, Guid? UserId)
    {
        var cardListExist = await context.CardLists.AnyAsync(cl => cl.Id == CardListId);

        if (cardListExist == false)
            return null;
        var newTimeRangEntity = new TimeRangeEntity()
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now,
            EndTime = Deadline,
        };
        await context.TimeRanges.AddAsync(newTimeRangEntity);

        var newCard = new CardEntity()
        {
            Id = Guid.NewGuid(),
            Description = Task,
            Status = Constants.Todo,
            IsCompleated = false,
            TimeRangeEntityId = newTimeRangEntity.Id,
            CardListId = CardListId,
            UserId = UserId
        };

        try
        {
            var cardId = await CreateAsync(newCard);
            return cardId;
        }
        catch (Exception)
        {
            return null;
        }

    }
}
