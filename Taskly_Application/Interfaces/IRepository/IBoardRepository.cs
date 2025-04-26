using Taskly_Application.DTO;
using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IBoardRepository : IRepository<BoardEntity>
{
    Task<BoardEntity> GetBoardByIdAsync(Guid boardId);
    Task AddMemberToBoardAsync(Guid boardId, Guid userId);
    Task RemoveMemberFromBoardAsync(Guid boardId, Guid userId);
    Task<IEnumerable<BoardTableMemberDto>> GetMembersOfBoardAsync(Guid boardId);
    
    Task AddCardListToBoardAsync(Guid boardId, CardListEntity cardList);
    Task RemoveCardListFromBoardAsync(Guid boardId, Guid cardListId);
    Task<ICollection<BoardEntity>?> GetBoardsByUser(Guid UserId);
    Task<Guid> GetIdOfCardsListByTitleAsync(Guid BoardId, string Title);
    Task<Guid?> AddCardToCardsListAsync(CardEntity card, Guid cardListId);
    Task<ICollection<CardListEntity>?> GetCardListEntityByBoardIdAsync(Guid BoardId);
}