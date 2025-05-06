using System.Reflection.Metadata;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Challenge;
using Taskly_Api.Response.Challenge;
using Taskly_Application.Requests.Challenge.Command.Book;
using Taskly_Application.Requests.Challenge.Command.Create;
using Taskly_Application.Requests.Challenge.Command.Delete;
using Taskly_Application.Requests.Challenge.Command.MarkAsCompleted;
using Taskly_Application.Requests.Challenge.Query.GetAll;
using Taskly_Application.Requests.Challenge.Query.GetAllActive;
using Taskly_Application.Requests.Challenge.Query.GetAllAvaliable;
using Taskly_Application.Requests.Challenge.Query.GetById;
using Taskly_Domain;

namespace Taskly_Api.Controllers;

[Route("api/challenge")]
[ApiController]
public class ChallengeController(ISender madiatr, IMapper mapper) : ApiController
{
    [HttpPost]
    // [Authorize(Roles = Constants.AdminRole)]
    public async Task<IActionResult> CreateChallenge([FromBody] CreateChallengeRequest request)
    {
        var command = mapper.Map<CreateChallengeCommand>(request);
        var result = await madiatr.Send(command);

        return result.Match(result => Ok(result),
            errors => Problem(errors));
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Constants.AdminRole)]
    public async Task<IActionResult> DeleteChallenge([FromRoute] Guid id)
    {
        var command = new DeleteChallengedCommand(id);
        var result = await madiatr.Send(command);

        return result.Match(result => Ok(result),
            errors => Problem(errors));
    }
    
    [HttpPut("mark-as-completed/{id:guid}")]
    public async Task<IActionResult> MarkAsCompleted([FromRoute] Guid id)
    {
        var command = new MarkChallengeAsCompletedCommand(id);
        var result = await madiatr.Send(command);

        return result.Match(result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPut("book-challenge/{id:guid}")]
    public async Task<IActionResult> BookChallenge([FromRoute] Guid id, [FromBody] Guid userId)
    {
        var command = new BookChallengeCommand(id, userId);
        var result = await madiatr.Send(command);
        
        return result.Match(result => Ok(result),
            errors => Problem(errors));
    }
    
    [HttpGet("get-challenges")]
    public async Task<IActionResult> GetChallenges()
    {
        var result = await madiatr.Send(new GetAllChallengesQuery());
        return result.Match(result => Ok(mapper.Map<ICollection<ChallengeResponse>>(result)),
            errors => Problem(errors));
    }
    
    [HttpGet("get-challenge-by-id/{id:guid}")]
    public async Task<IActionResult> GetChallengeById([FromRoute] Guid id)
    {
        var result = await madiatr.Send(new GetChallengeByIdQuery(id));
        return result.Match(result => Ok(mapper.Map<ChallengeResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpGet("get-active-challenges")]
    public async Task<IActionResult> GetActiveChallenges()
    {
        var result = await madiatr.Send(new GetAllActiveChallengesQuery());
        return result.Match(result => Ok(mapper.Map<ICollection<ChallengeResponse>>(result)),
            errors => Problem(errors));
    }
    
    [HttpGet("get-available-challenges")]
    public async Task<IActionResult> GetAvailableChallenges()
    {
        var result = await madiatr.Send(new GetAllAvaliableChallengesQuery());
        return result.Match(result => Ok(mapper.Map<ICollection<ChallengeResponse>>(result)),
            errors => Problem(errors));
    }
}