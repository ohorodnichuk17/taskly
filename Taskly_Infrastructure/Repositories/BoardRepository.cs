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
        board.Members.Add(user);

        await SaveChangesAsync("Error adding member to the board.");
    }

    public async Task RemoveMemberFromBoard(Guid boardId, Guid userId)
    {
        var (board, user) = await GetBoardAndUserAsync(boardId, userId);

        ValidateBoardMembers(board);

        board.Members.Remove(user);

        await SaveChangesAsync("Error removing member from the board.");
    }
    
    private async Task<(BoardEntity board, UserEntity user)> GetBoardAndUserAsync(Guid boardId, Guid userId)
    {
        if (boardId == Guid.Empty || userId == Guid.Empty)
            throw new ArgumentException("BoardId and UserId must not be empty");

        var board = await context.Boards
            .Include(bt => bt.Members)
            .FirstOrDefaultAsync(b => b.Id == boardId);
        if (board == null)
            throw new KeyNotFoundException("Board not found");

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

    public Task<IEnumerable<UserEntity>> GetMembersOfBoard(Guid boardId)
    {
        throw new NotImplementedException();
    }

    public Task AddCardToBoard(Guid boardId, CardEntity card)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCardFromBoard(Guid boardId, Guid cardId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBoardTemplate(Guid boardId, string template)
    {
        throw new NotImplementedException();
    }
}