using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Taskly_Application.DTO;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class BoardRepository(TasklyDbContext context): Repository<BoardEntity>(context), IBoardRepository
{
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

        /*board.Members ??= new List<UserEntity>();
        board.Members.Add(user);

        user.Boards.Add(board);*/

        var boardUserRelations = new Dictionary<string, object>();
        boardUserRelations.Add("BoardId", board.Id);
        boardUserRelations.Add("UserId", user.Id);
        await context.Set<Dictionary<string, object>>("UserBoard").AddAsync(boardUserRelations);

        await context.SaveChangesAsync();
    }
    public async Task<bool> IsUserOnTheBoardAsync(Guid BoardId, Guid UserId)
    {
        try
        {
            var board = await GetBoardByIdAsync(BoardId);
            ValidateBoardMembers(board);
            if (board.Members!.Any(m => m.Id == UserId))
                return true;

            return false;
        }
        catch (Exception)
        {
            return true;
        }
        
    }
    public async Task RemoveMemberFromBoardAsync(Guid boardId, Guid userId)
    {
        var (board, user) = await GetBoardAndUserAsync(boardId, userId);
        
        if (board.Members == null)
            throw new InvalidOperationException("Board members list is not initialized.");

        var boardUser = await context.Set<Dictionary<string, object>>("UserBoard")
            .FirstOrDefaultAsync(bu => (Guid)bu["BoardId"] == board.Id && (Guid)bu["UserId"] == user.Id);

        if(boardUser != null)
            context.Set<Dictionary<string, object>>("UserBoard").Remove(boardUser);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<BoardTableMemberDto>> GetMembersOfBoardAsync(Guid boardId)
    {
        var board = await GetBoardByIdAsync(boardId);
        ValidateBoardMembers(board);
        return board.Members.Select(member => new BoardTableMemberDto
        {
            Email = member.Email,
            AvatarId = member.AvatarId
        }) ?? Enumerable.Empty<BoardTableMemberDto>();
    }

    public async Task AddCardListToBoardAsync(Guid boardId, CardListEntity cardList)
    {
        if(cardList == null)
            throw new ArgumentNullException(nameof(cardList), "CardList must not be null");
        var board = await GetBoardByIdAsync(boardId);
        board.CardLists.Add(cardList);
        await context.SaveChangesAsync();
    }

    public async Task<Guid> GetIdOfCardsListByTitleAsync(Guid BoardId,string Title)
    {
        var cardList = await context.CardLists.FirstOrDefaultAsync(cl => cl.BoardId == BoardId && cl.Title == Title);

        if(cardList == null)
        {
            cardList = new CardListEntity() { 
                Id = Guid.NewGuid(),
                Title = Title,
                BoardId = BoardId     
            };
            await context.CardLists.AddAsync(cardList);
            await context.SaveChangesAsync();
        }     

        return cardList.Id;
    }
    private async Task<CardListEntity?> GetCardListByIdAsync(Guid cardListId)
    {
        return await context.CardLists.FirstOrDefaultAsync(cl => cl.Id == cardListId);
    }
    
    public async Task<Guid?> AddCardToCardsListAsync(CardEntity card, Guid cardListId)
    {
        if (card == null) return null;

        var cardList = GetCardListByIdAsync(cardListId);

        if (cardList == null) return null;

        card.CardListId = cardListId;
        

        return card.Id;
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

    public async Task<ICollection<BoardEntity>?> GetBoardsByUserAsync(Guid UserId)
    {
        var boardsId = context.Set<Dictionary<string, object>>("UserBoard")
                    .Where(ub => (Guid)ub["UserId"] == UserId)
                    .Select(ub => (Guid)ub["BoardId"]);

        var boards = await context.Boards
                                        .Where(b => boardsId.Any(_b => b.Id == _b))
                                        .Include(b => b.Members)
                                        .Include(b => b.BoardTemplate)
                                        .ToListAsync();

        return boards;
    }

    public async Task<ICollection<CardListEntity>?> GetCardListEntityByBoardIdAsync(Guid BoardId)
    {
        try
        {
            var board = await GetBoardByIdAsync(BoardId);

            return board.CardLists;
        }
        catch (Exception ex)
        {
            return null;
        }    
    }

    public async Task<bool> IsUserHasBoardByIdAsync(Guid BoardId, Guid UserId)
    {
        var board = await GetBoardByIdAsync(BoardId);

        return board.Members == null ? false : board.Members.Any(m => m.Id == UserId);
    }

    public async Task<ICollection<Guid>?> LeaveBoardAsync(Guid BoardId, Guid UserId)
    {
        try
        {
            var includedBoard = await GetBoardByIdAsync(BoardId);
            if(includedBoard.Members != null && includedBoard.Members.Any(user => user.Id == UserId)) 
            {
                var user = await context.Users.FirstOrDefaultAsync(user => user.Id == UserId);
                
                await RemoveMemberFromBoardAsync(BoardId, UserId);

                if (includedBoard.Members.Count > 1)
                {
                    var cardLists = context.CardLists.Where(cl => cl.BoardId == BoardId).Select(cl => cl.Id);
                    var cards = context.Cards.Where(card => card.UserId == UserId && cardLists.Any(cl => cl == card.CardListId));
                    var returnedList = await cards.Select(c => c.Id).ToListAsync();

                    foreach (var card in cards)
                    {
                        card.UserId = null;
                        context.Cards.Update(card);
                    }
                    await context.SaveChangesAsync();
                    
                    
                    return returnedList;
                }
                else
                {
                    await DeleteAsync(BoardId);
                }
                return new List<Guid>();
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
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
            .ThenInclude(cr => cr.User)
            .ThenInclude(u => u.Avatar)
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