using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Challenge;
using Taskly_Api.Response.Badge;
using Taskly_Api.Response.Challenge;
using Taskly_Application.Requests.Badge.Query.GetAll;
using Taskly_Application.Requests.Badge.Query.GetById;
using Taskly_Application.Requests.Challenge.Command.Book;
using Taskly_Application.Requests.Challenge.Command.Create;
using Taskly_Application.Requests.Challenge.Command.Delete;
using Taskly_Application.Requests.Challenge.Command.MarkAsCompleted;
using Taskly_Application.Requests.Challenge.Query.GetAll;
using Taskly_Application.Requests.Challenge.Query.GetAllActive;
using Taskly_Application.Requests.Challenge.Query.GetAllAvaliable;
using Taskly_Application.Requests.Challenge.Query.GetById;
using Taskly_Application.Requests.UserLevel.Query.GetByUserId;

namespace Taskly_Api.Controllers;

[ApiController]
[Route("api/gamification")]
public class GamificationController(ISender mediatr, IMapper mapper)
    : ApiController
{
    [HttpGet("get-all-badges")]
    public async Task<IActionResult> GetAllBadges()
    {
        var result = await mediatr.Send(new GetAllBadgesQuery());
        return result.Match(
            badges => Ok(mapper.Map<IEnumerable<BadgeResponse>>(badges)),
            errors => Problem(errors));
    }
    
    [HttpGet("get-badge-by-id/{id:guid}")]
    public async Task<IActionResult> GetBadgeById(Guid id)
    {
        var result = await mediatr.Send(new GetBadgeByIdQuery(id));
        return result.Match(
            badge => Ok(mapper.Map<BadgeResponse>(badge)),
            errors => Problem(errors));
    }
    
    [HttpGet("get-user-level/{userId:guid}")]
    public async Task<IActionResult> GetUserLevel([FromRoute] Guid userId)
    {
        var result = await mediatr.Send(new GetUserLevelByUserIdQuery(userId));
        return result.Match(
            level => Ok(level),
            errors => Problem(errors));
    }
    
    [HttpPost("create-challenge")]
    // [Authorize(Roles = Constants.AdminRole)]
    public async Task<IActionResult> CreateChallenge([FromBody] CreateChallengeRequest request)
    {
        var command = mapper.Map<CreateChallengeCommand>(request);
        var result = await mediatr.Send(command);

        return result.Match(result => Ok(result),
            errors => Problem(errors));
    }
    
    [HttpDelete("delete-challenge/{id:guid}")]
    // [Authorize(Roles = Constants.AdminRole)]
    public async Task<IActionResult> DeleteChallenge([FromRoute] Guid id)
    {
        var command = new DeleteChallengedCommand(id);
        var result = await mediatr.Send(command);

        return result.Match(result => Ok(result),
            errors => Problem(errors));
    }
    
    [HttpPut("mark-challenge-as-completed/{id:guid}")]
    public async Task<IActionResult> MarkAsCompleted([FromRoute] Guid id)
    {
        var command = new MarkChallengeAsCompletedCommand(id);
        var result = await mediatr.Send(command);

        return result.Match(result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPut("book-challenge/{id:guid}")]
    public async Task<IActionResult> BookChallenge([FromRoute] Guid id, [FromBody] Guid userId)
    {
        var command = new BookChallengeCommand(id, userId);
        var result = await mediatr.Send(command);
        
        return result.Match(result => Ok(result),
            errors => Problem(errors));
    }
    
    [HttpGet("get-challenges")]
    public async Task<IActionResult> GetChallenges()
    {
        var result = await mediatr.Send(new GetAllChallengesQuery());
        return result.Match(result => Ok(mapper.Map<ICollection<ChallengeResponse>>(result)),
            errors => Problem(errors));
    }
    
    [HttpGet("get-challenge-by-id/{id:guid}")]
    public async Task<IActionResult> GetChallengeById([FromRoute] Guid id)
    {
        var result = await mediatr.Send(new GetChallengeByIdQuery(id));
        return result.Match(result => Ok(mapper.Map<ChallengeResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpGet("get-active-challenges")]
    public async Task<IActionResult> GetActiveChallenges()
    {
        var result = await mediatr.Send(new GetAllActiveChallengesQuery());
        return result.Match(result => Ok(mapper.Map<ICollection<ChallengeResponse>>(result)),
            errors => Problem(errors));
    }
    
    [HttpGet("get-available-challenges")]
    public async Task<IActionResult> GetAvailableChallenges()
    {
        var result = await mediatr.Send(new GetAllAvaliableChallengesQuery());
        return result.Match(result => Ok(mapper.Map<ICollection<ChallengeResponse>>(result)),
            errors => Problem(errors));
    }
}