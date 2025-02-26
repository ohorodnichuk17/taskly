using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Taskly_Application.DTO.MembersOfBoardDTO;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class BoardRepository(TasklyDbContext context): Repository<BoardEntity>(context), IBoardRepository
{
    public async Task<BoardEntity> GetTemplateBoardAsync() => 
        await GetBoardByConditionAsync(b => b.Tag == "Template");

    public async Task<BoardEntity> GetBoardByIdAsync(Guid boardId)
    {
        if (boardId == Guid.Empty)
            throw new ArgumentException("BoardId must not be empty");

        return await GetBoardByConditionAsync(b => b.Id == boardId);
    }

    public async Task AddMemberToBoardAsync(Guid boardId, Guid userId)
    {
        var (board, user) = await GetBoardAndUserAsync(boardId, userId);
        ValidateBoardMembers(board);
        board.IsTeamBoard = true;

        board.Members ??= new List<UserEntity>();
        board.Members.Add(user);
<<<<<<< HEAD
        //await SaveAsync(board);
=======
>>>>>>> f58c923d5a03af2fed1db6be4ff56ebd9e297487
        user.Boards.Add(board);

        var boardUserRelations = new Dictionary<string, object>();
        boardUserRelations.Add("BoardId", board.Id);
        boardUserRelations.Add("UserId", user.Id);
        await context.Set<Dictionary<string, object>>("UserBoard").AddAsync(boardUserRelations);
<<<<<<< HEAD

=======
        
>>>>>>> f58c923d5a03af2fed1db6be4ff56ebd9e297487
        await context.SaveChangesAsync();
    }

    public async Task RemoveMemberFromBoardAsync(Guid boardId, Guid userId)
    {
        var (board, user) = await GetBoardAndUserAsync(boardId, userId);
        ValidateBoardMembers(board);
        if (board.Members == null)
            throw new InvalidOperationException("Board members list is not initialized.");

        var boardUser = await context.Set<Dictionary<string, object>>("UserBoard")
            .FirstOrDefaultAsync(bu => (Guid)bu["BoardId"] == board.Id && (Guid)bu["UserId"] == user.Id);

        if(boardUser != null)
            context.Set<Dictionary<string, object>>("UserBoard").Remove(boardUser);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MembersOfBoardDTO>> GetMembersOfBoardAsync(Guid boardId)
    {
        var board = await GetBoardByIdAsync(boardId);
        ValidateBoardMembers(board);
        return board.Members.Select(member => new MembersOfBoardDTO
        {
            UserName = member.UserName,
            Email = member.Email,
            AvatarId = member.AvatarId
        }) ?? Enumerable.Empty<MembersOfBoardDTO>();
    }

    public async Task AddCardListToBoardAsync(Guid boardId, CardListEntity cardList)
    {
        if(cardList == null)
            throw new ArgumentNullException(nameof(cardList), "CardList must not be null");
        var board = await GetBoardByIdAsync(boardId);
        board.CardLists.Add(cardList);
        await context.SaveChangesAsync();
    }

    public async Task RemoveCardListFromBoardAsync(Guid boardId, Guid cardListId)
    {
        var board = await GetBoardByIdAsync(boardId);
        if(cardListId == Guid.Empty)
            throw new ArgumentException("CardListId must not be empty");
        var cardList = board.CardLists.FirstOrDefault(c => c.Id == cardListId);
        if(cardList == null)
            throw new KeyNotFoundException("CardList not found");
        board.CardLists.Remove(cardList);
        await context.SaveChangesAsync();
    }
    
    private async Task<(BoardEntity board, UserEntity user)> GetBoardAndUserAsync(Guid boardId, Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("BoardId and UserId must not be empty");
        var board = await GetBoardByIdAsync(boardId);
        var user = await context.Users.FindAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("User not found");
        return (board, user);
    }
    
    private void ValidateBoardMembers(BoardEntity board)
    {
        if (board.Members == null)
            throw new InvalidOperationException("Board team is not initialized");
    }
    
    private async Task<BoardEntity> GetBoardByConditionAsync(Expression<Func<BoardEntity, bool>> condition)
    {
        var board = await context.Boards
            .Include(b => b.Members)
            .Include(b => b.BoardTemplate)
            .Include(b => b.CardLists)
            .ThenInclude(cl => cl.Cards)
            .ThenInclude(c => c.Comments)
            .Include(b => b.CardLists)
            .ThenInclude(cl => cl.Cards)
            .ThenInclude(c => c.TimeRangeEntity)
            .FirstOrDefaultAsync(condition);

        if (board == null)
            throw new KeyNotFoundException("Board not found");

        return board;
    }
}