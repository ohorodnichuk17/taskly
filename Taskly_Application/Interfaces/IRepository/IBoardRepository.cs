using Taskly_Application.DTO;
using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IBoardRepository : IRepository<BoardEntity>
{
    Task<BoardEntity> GetBoardByIdAsync(Guid boardId);
    Task AddMemberToBoardAsync(Guid boardId, Guid userId);
    Task<bool> IsUserOnTheBoardAsync(Guid BoardId, Guid UserId);
    Task<ICollection<Guid>> RemoveMemberFromBoardAsync(Guid boardId, Guid userId);
    Task<IEnumerable<BoardMemberDto>> GetMembersOfBoardAsync(Guid boardId);
    
    Task AddCardListToBoardAsync(Guid boardId, CardListEntity cardList);
    Task RemoveCardListFromBoardAsync(Guid boardId, Guid cardListId);
    Task<ICollection<BoardEntity>?> GetBoardsByUserAsync(Guid UserId);
    Task<bool> IsUserHasBoardByIdAsync(Guid BoardId, Guid UserId);
    Task<Guid> GetIdOfCardsListByTitleAsync(Guid BoardId, string Title);
    Task<Guid?> AddCardToCardsListAsync(CardEntity card, Guid cardListId);
    Task<ICollection<CardListEntity>?> GetCardListEntityByBoardIdAsync(Guid BoardId);
    Task<ICollection<Guid>?> LeaveBoardAsync(Guid BoardId, Guid UserId);
}