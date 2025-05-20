using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasklySender.Request;
using TasklySender_Application.Requests.Command.SendMail;

namespace TasklySender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController(ISender sender,IMapper mapper) : ApiController(mapper)
    {
        [HttpPost("send-mail")]
        public async Task<IActionResult> SendMail([FromBody] SendMailRequest request)
        {
            var result = await sender.Send(new SendMailCommand(request.TypeOfHTML, request.To, request.Props));

            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
    }
}
