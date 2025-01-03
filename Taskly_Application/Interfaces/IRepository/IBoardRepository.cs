using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IBoardRepository : IRepository<BoardEntity>
{
    Task AddMemberToBoard(Guid boardId, Guid userId);
    Task RemoveMemberFromBoard(Guid boardId, Guid userId);
    Task<IEnumerable<UserEntity>> GetMembersOfBoard(Guid boardId);
    
    Task AddCardListToBoard(Guid boardId, CardListEntity cardList);
    Task RemoveCardListFromBoard(Guid boardId, Guid cardListId);
}