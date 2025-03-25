using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface ICardRepository : IRepository<CardEntity>
{
    Task<Guid?> TransferCardToAnotherCardListAsync(Guid CardListId, Guid CardId);
}
