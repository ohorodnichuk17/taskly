using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Response.Badge;
using Taskly_Application.Requests.Badge.Query.GetAll;
using Taskly_Application.Requests.Badge.Query.GetById;

namespace Taskly_Api.Controllers;

[ApiController]
[Route("api/badge")]
public class BadgeController(ISender mediatr, IMapper mapper)
    : ApiController
{
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllBadges()
    {
        var result = await mediatr.Send(new GetAllBadgesQuery());
        return result.Match(
            badges => Ok(mapper.Map<IEnumerable<BadgeResponse>>(badges)),
            errors => Problem(errors));
    }
    
    [HttpGet("get-by-id/{id:guid}")]
    public async Task<IActionResult> GetBadgeById(Guid id)
    {
        var result = await mediatr.Send(new GetBadgeByIdQuery(id));
        return result.Match(
            badge => Ok(mapper.Map<BadgeResponse>(badge)),
            errors => Problem(errors));
    }
}