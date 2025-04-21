using Taskly_Domain.Entities;
using Taskly_Domain.Other;

namespace Taskly_Application.Interfaces.IRepository;

public interface ICardRepository : IRepository<CardEntity>
{
    Task<Guid?> TransferCardToAnotherCardListAsync(Guid CardListId, Guid CardId);
    Task RemoveCardFromBoardAsync(Guid CardId);
    Task<Guid?> ChangeCardAsync(Guid CardId, ChangeCardProps ChangeCardProps);
    Task<Guid?> LeaveCardAsync(Guid CardId);
    Task<Guid?> TakeCardAsync(Guid CardId, Guid UserId);
}
