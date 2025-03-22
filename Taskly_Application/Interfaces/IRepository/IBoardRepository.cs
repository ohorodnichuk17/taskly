using Taskly_Application.DTO.MembersOfBoardDTO;
using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IBoardRepository : IRepository<BoardEntity>
{
    Task<BoardEntity> GetTemplateBoardAsync();
    Task<BoardEntity> GetBoardByIdAsync(Guid boardId);
    Task AddMemberToBoardAsync(Guid boardId, Guid userId);
    Task RemoveMemberFromBoardAsync(Guid boardId, Guid userId);
    Task<IEnumerable<MembersOfBoardDTO>> GetMembersOfBoardAsync(Guid boardId);
    
    Task AddCardListToBoardAsync(Guid boardId, CardListEntity cardList);
    Task RemoveCardListFromBoardAsync(Guid boardId, Guid cardListId);
    Task<ICollection<BoardEntity>?> GetBoardsByUser(Guid UserId);
    Task<Guid> GetIdOfCardsListByTitleAsync(Guid BoardId, string Title);
    Task<Guid?> AddCardToCardsListAsync(CardEntity card, Guid cardListId);
    Task CreateCardAsync(string Description, string Status, Guid CardListId);
    Task CreateCardAsync(ICollection<string> Descriptions, string Status, Guid CardListId);
    Task<ICollection<CardListEntity>?> GetCardListEntityByBoardIdAsync(Guid BoardId);
}