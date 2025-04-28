using Taskly_Domain.Entities;
using Taskly_Domain.ValueObjects;

namespace Taskly_Application.Interfaces.IRepository;

public interface ICardRepository : IRepository<CardEntity>
{
    Task<Guid?> TransferCardToAnotherCardListAsync(Guid CardListId, Guid CardId);
    Task RemoveCardFromBoardAsync(Guid CardId);
    Task<Guid?> ChangeCardAsync(Guid CardId, ChangeCardProps ChangeCardProps);
    Task<Guid?> LeaveCardAsync(Guid CardId);
    Task<Guid?> TakeCardAsync(Guid CardId, Guid UserId);
    Task<Guid?> CreateCardAsync(Guid CardListId, string Task, DateTime Deadline, Guid? UserId);
    Task<CardEntity[]> CreateCardAsync(ICollection<string> Descriptions, string Status, Guid CardListId, Guid UserId);
}
