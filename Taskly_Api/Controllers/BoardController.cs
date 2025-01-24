using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
}