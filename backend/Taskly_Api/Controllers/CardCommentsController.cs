using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Response.CardComments;
using Taskly_Application.Requests.CardComment.Query.GetCommentsByCardId;

namespace Taskly_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardCommentsController(ISender sender, IMapper mapper) : ApiController
    {
        [HttpGet("get-card-comments-by-card-id-{id}")]
        public async Task<IActionResult> GetCommentsByCardId(Guid id)
        {
            var result = await sender.Send(new GetCommentsByCardIdQuery(id));

            return result.Match(result => Ok(mapper.Map<CardCommentResponse[]>(result)),
                errors=> Problem(errors));
        }
    }
}
