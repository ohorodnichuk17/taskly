using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Feedback;
using Taskly_Application.Requests.Feedback.Command.Create;
using Taskly_Application.Requests.Feedback.Query.GetAll;
using Taskly_Application.Requests.Feedback.Query.GetById;

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
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllFeedbacks()
    {
        var query = new GetAllFeedbacksQuery();

        var result = await mediatr.Send(query);

        return result.Match(
            feedbacks => Ok(feedbacks),
            errors => Problem(errors));
    }
    
    [HttpGet("get-by-id/{feedbackId}")]
    public async Task<IActionResult> GetFeedbackById([FromRoute] Guid feedbackId)
    {
        var query = new GetFeedbackByIdQuery(feedbackId);

        var result = await mediatr.Send(query);

        return result.Match(
            feedback => Ok(feedback),
            errors => Problem(errors));
    }
}