using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Board;
using Taskly_Api.Response.Board;
using Taskly_Application.Requests.Board.Command.AddMemberToBoard;
using Taskly_Application.Requests.Board.Command.CreateBoard;
using Taskly_Application.Requests.Board.Command.DeleteBoard;
using Taskly_Application.Requests.Board.Command.LeaveBoard;
using Taskly_Application.Requests.Board.Command.RemoveMemberFromBoard;
using Taskly_Application.Requests.Board.Query.GetAllBoards;
using Taskly_Application.Requests.Board.Query.GetBoardById;
using Taskly_Application.Requests.Board.Query.GetBoardsByUser;
using Taskly_Application.Requests.Board.Query.GetMembersOfBoard;
using Taskly_Application.Requests.Board.Query.GetTemplatesOfBoard;

namespace Taskly_Api.Controllers;

[Route("api/board")]
[ApiController]
public class BoardController(ISender sender, IMapper mapper) : ApiController
{
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetBoardById(Guid id)
    {
        var result = await sender.Send(new GetBoardByIdQuery(id));

        return result.Match(r => Ok(r),
            errors => Problem(errors));
    }
    
    [HttpGet("getAll")]
    [Authorize]
    public async Task<IActionResult> GetAllBoards()
    {
        var result = await sender.Send(new GetAllBoardsQuery());

        return result.Match(r => Ok(r),
            errors => Problem(errors));
    }
    
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateBoard([FromBody] CreateBoardRequest request)
    {
        var res = await sender.Send(
            mapper.Map<CreateBoardCommand>(request));
        return res.Match(result => Ok(result),
            errors => Problem(errors));
    }
    
    [HttpDelete("delete/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBoard(Guid id)
    {
        var res = await sender.Send(new DeleteBoardCommand(id));
        return res.Match(result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPost("add-member")]
    [Authorize]
    public async Task<IActionResult> AddMemberToBoard([FromBody] MemberToBoardRequest request)
    {
        var res = await sender.Send(mapper.Map<AddMemberToBoardCommand>(request));
        Console.WriteLine("AVATAR --------------- ",res.Value.Avatar!.ImagePath);
        return res.Match(result => Ok(mapper.Map<MemberOfBoardResponse>(result)),
            errors => Problem(errors));
    }

    [HttpDelete("remove-member")]
    [Authorize]
    public async Task<IActionResult> RemoveMemberFromBoard([FromBody] RemoveMemberFromBoardRequest request)
    {
        var res = await sender.Send(mapper.Map<RemoveMemberFromBoardCommand>(request));
        return res.Match(result => Ok(result),
            errors => Problem(errors));
    }
    
    [HttpGet("members/{boardId}")]
    [Authorize]
    public async Task<IActionResult> GetMembersOfBoard(Guid boardId)
    {
        var result = await sender.Send(new GetMembersOfBoardQuery(boardId));
        return result.Match(r => Ok(mapper.Map<MemberOfBoardResponse[]>(r.ToArray())),
            errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("get-boards-by-user")]
    public async Task<IActionResult> GetBoardsByUser()
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")!.Value;

        var boards = await sender.Send(new GetBoardsByUserQuery(Guid.Parse(userId)));

        return boards.Match(boards => Ok(mapper.Map<ICollection<UsersBoardResponse>>(boards)),
            errors => Problem(errors));
    }

    [Authorize]
    [HttpPut("leave-board")]
    public async Task<IActionResult> LeaveBoard([FromBody] LeaveBoardReqeust leaveBoardReqeust)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")!.Value;

        var result = await sender.Send(new LeaveBoardCommand(leaveBoardReqeust.BoardId, Guid.Parse(userId)));
        Console.WriteLine($"Leave board cards - {result.Value.Length}");

        return result.Match(result => Ok(result),
            errors => Problem(errors));
    }


    [Authorize]
    [HttpGet("get-templates-of-board")]
    public async Task<IActionResult> GetTemplatesOfBoard()
    {
        var result = await sender.Send(new GetTemplatesOfBoardQuery());

        return Ok(result);
    }
}