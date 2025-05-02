using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Feedback;
using Taskly_Application.Requests.Feedback.Command.Create;

namespace Taskly_Api.Controllers;

[Route("api/feedback")]
[ApiController]
public class FeedbackController(ISender mediatr) : ApiController
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackRequest request)
    {
        var command = new CreateFeedbackCommand(
            request.UserId,
            request.Review,
            request.Rating);

        var result = await mediatr.Send(command);

        return result.Match(
            feedback => Ok(feedback),
            errors => Problem(errors));
    }
}