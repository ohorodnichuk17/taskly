using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IBoardRepository : IRepository<BoardEntity>
{
    // Task<BoardEntity> CreateDefaultBoardAsync(BoardEntity board);
    Task<BoardEntity> GetTemplateBoardAsync(Guid boardId);
    Task AddMemberToBoardAsync(Guid boardId, Guid userId);
    Task RemoveMemberFromBoardAsync(Guid boardId, Guid userId);
    Task<IEnumerable<UserEntity>> GetMembersOfBoardAsync(Guid boardId);
    
    Task AddCardListToBoardAsync(Guid boardId, CardListEntity cardList);
    Task RemoveCardListFromBoardAsync(Guid boardId, Guid cardListId);
}