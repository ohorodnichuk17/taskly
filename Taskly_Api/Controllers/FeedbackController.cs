using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Feedback;
using Taskly_Api.Response.Feedback;
using Taskly_Application.Requests.Feedback.Command.Create;
using Taskly_Application.Requests.Feedback.Command.Delete;
using Taskly_Application.Requests.Feedback.Query.GetAll;
using Taskly_Application.Requests.Feedback.Query.GetById;
using Microsoft.AspNet.SignalR;

namespace Taskly_Api.Controllers;

[Route("api/feedback")]
[ApiController]
public class FeedbackController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackRequest request)
    {
        var command = new CreateFeedbackCommand(
            request.UserId,
            request.Review,
            request.Rating);

        var result = await mediatr.Send(mapper.Map<CreateFeedbackCommand>(request));

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
            feedbacks => Ok(mapper.Map<IEnumerable<FeedbackResponse>>(feedbacks)),
            errors => Problem(errors));
    }
    
    [HttpGet("get-by-id/{feedbackId}")]
    public async Task<IActionResult> GetFeedbackById([FromRoute] Guid feedbackId)
    {
        var query = new GetFeedbackByIdQuery(feedbackId);

        var result = await mediatr.Send(query);

        return result.Match(
            feedback => Ok(mapper.Map<FeedbackResponse>(feedback)),
            errors => Problem(errors));
    }
    
    [HttpDelete("delete/{feedbackId}")]
    [Authorize]
    public async Task<IActionResult> DeleteFeedback([FromRoute] Guid feedbackId)
    {
        var command = new DeleteFeedbackCommand(feedbackId);

        var result = await mediatr.Send(command);

        return result.Match(
            isDeleted => Ok(isDeleted),
            errors => Problem(errors));
    }
}