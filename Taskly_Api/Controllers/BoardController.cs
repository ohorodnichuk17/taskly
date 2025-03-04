using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Board;
using Taskly_Api.Response.Board;
using Taskly_Application.Requests.Board.Command.AddMemberToBoard;
using Taskly_Application.Requests.Board.Command.CreateBoard;
using Taskly_Application.Requests.Board.Command.DeleteBoard;
using Taskly_Application.Requests.Board.Command.RemoveMemberFromBoard;
using Taskly_Application.Requests.Board.Query.GetAllBoards;
using Taskly_Application.Requests.Board.Query.GetBoardById;
using Taskly_Application.Requests.Board.Query.GetBoardsByUser;
using Taskly_Application.Requests.Board.Query.GetMembersOfBoard;
using Taskly_Application.Requests.Board.Query.GetTemplateBoard;

namespace Taskly_Api.Controllers;

[Route("api/board")]
[ApiController]
public class BoardController(ISender sender, IMapper mapper) : ApiController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBoardById(Guid id)
    {
        var result = await sender.Send(new GetBoardByIdQuery(id));

        return result.Match(r => Ok(r),
            errors => Problem(errors));
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllBoards()
    {
        var result = await sender.Send(new GetAllBoardsQuery());

        return result.Match(r => Ok(r),
            errors => Problem(errors));
    }
    
    [HttpGet("getTemplateBoard")]
    public async Task<IActionResult> GetTemplateBoard()
    {
        var result = await sender.Send(new GetTemplateBoardQuery());

        return result.Match(r => Ok(r),
            errors => Problem(errors));
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateBoard([FromBody] CreateBoardRequest request)
    {
        var res = await sender.Send(
            mapper.Map<CreateBoardCommand>(request));
        return res.Match(result => Ok(result),
            errors => Problem(errors));
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBoard(Guid id)
    {
        var res = await sender.Send(new DeleteBoardCommand(id));
        return res.Match(result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPost("add-member")]
    public async Task<IActionResult> AddMemberToBoard([FromBody] MemberToBoardRequest request)
    {
        var res = await sender.Send(mapper.Map<AddMemberToBoardCommand>(request));
        return res.Match(result => Ok(result),
            errors => Problem(errors));
    }

    [HttpDelete("remove-member")]
    public async Task<IActionResult> RemoveMemberFromBoard([FromBody] MemberToBoardRequest request)
    {
        var res = await sender.Send(mapper.Map<RemoveMemberFromBoardCommand>(request));
        return res.Match(result => Ok(result),
            errors => Problem(errors));
    }
    
    [HttpGet("members/{boardId}")]
    public async Task<IActionResult> GetMembersOfBoard(Guid boardId)
    {
        var result = await sender.Send(new GetMembersOfBoardQuery(boardId));
        return result.Match(r => Ok(r),
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
}