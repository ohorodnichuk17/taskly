using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Authenticate;
using MapsterMapper;
using Taskly_Application.Requests.Authentication.Command.SendVerificationEmail;
using Taskly_Application.Requests.Authentication.Command.VerificateEmail;
using Taskly_Application.Requests.Authentication.Command.Register;
using Taskly_Application.Requests.Authentication.Query.Login;

namespace Taskly_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;
        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] SendVerificationCodeRequest sendVerificationCodeRequest)
        {
            var result = await _sender.Send(_mapper.Map<SendVerificationCodeCommand>(sendVerificationCodeRequest));

            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost("verificate-email")]
        public async Task<IActionResult> VerificateEmail([FromBody] VerificateEmailRequest verificateEmailRequest)
        {
            var result = await _sender.Send(_mapper.Map<VerificateEmailCommand>(verificateEmailRequest));

            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var result = await _sender.Send(_mapper.Map<RegisterCommand>(registerRequest));

            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _sender.Send(_mapper.Map<LoginQuery>(loginRequest));

            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
    }
}
