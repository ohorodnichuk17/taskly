using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class BoardRepository(TasklyDbContext context): Repository<BoardEntity>(context), IBoardRepository
{
    public async Task AddMemberToBoard(Guid boardId, Guid userId)
    {
        var (board, user) = await GetBoardAndUserAsync(boardId, userId);
        ValidateBoardMembers(board);
        board.IsTeamBoard = true;
        board.Members ??= new List<UserEntity>();
        board.Members.Add(user);
        await SaveChangesAsync("Error adding member to the board.");
    }

    public async Task RemoveMemberFromBoard(Guid boardId, Guid userId)
    {
        var (board, user) = await GetBoardAndUserAsync(boardId, userId);
        ValidateBoardMembers(board);
        if (board.Members == null)
            throw new InvalidOperationException("Board members list is not initialized.");
        if (!board.Members.Contains(user))
            throw new InvalidOperationException($"User {userId} is not a member of the board.");
        board.Members.Remove(user);
        await SaveChangesAsync("Error removing member from the board.");
    }

    public async Task<IEnumerable<UserEntity>> GetMembersOfBoard(Guid boardId)
    {
        var board = await GetBoard(boardId);
        ValidateBoardMembers(board);
        return board.Members ?? Enumerable.Empty<UserEntity>();
    }

    public async Task AddCardListToBoard(Guid boardId, CardListEntity cardList)
    {
        if(cardList == null)
            throw new ArgumentNullException(nameof(cardList), "CardList must not be null");
        var board = await GetBoard(boardId);
        board.CardLists.Add(cardList);
        await SaveChangesAsync("Error adding CardList to the board.");
    }

    public async Task RemoveCardListFromBoard(Guid boardId, Guid cardListId)
    {
        var board = await GetBoard(boardId);
        if(cardListId == Guid.Empty)
            throw new ArgumentException("CardListId must not be empty");
        var cardList = board.CardLists.FirstOrDefault(c => c.Id == cardListId);
        if(cardList == null)
            throw new KeyNotFoundException("CardList not found");
        board.CardLists.Remove(cardList);
        await SaveChangesAsync("Error removing card list from the board.");
    }
    
    private async Task<(BoardEntity board, UserEntity user)> GetBoardAndUserAsync(Guid boardId, Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("BoardId and UserId must not be empty");
        var board = await GetBoard(boardId);
        var user = await context.Users.FindAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("User not found");
        return (board, user);
    }
    
    private async Task SaveChangesAsync(string errorMessage)
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException(errorMessage, ex);
        }
    }
    
    private void ValidateBoardMembers(BoardEntity board)
    {
        if (board.Members == null)
            throw new InvalidOperationException("Board team is not initialized");
    }

    private async Task<BoardEntity> GetBoard(Guid boardId)
    {
        if (boardId == Guid.Empty)
            throw new ArgumentException("BoardId must not be empty");
        var board = await context.Boards
            .Include(m => m.Members)
            .FirstOrDefaultAsync(b => b.Id == boardId);
        if (board == null)
            throw new KeyNotFoundException("Board not found");
        return board;
    }
}