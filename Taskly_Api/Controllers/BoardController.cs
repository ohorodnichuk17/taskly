using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Board;
using Taskly_Application.Requests.Board.Command.CreateBoard;
using Taskly_Application.Requests.Board.Query.GetBoardById;
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
}