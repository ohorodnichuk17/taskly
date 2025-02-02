using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Authenticate;
using MapsterMapper;
using Taskly_Application.Requests.Authentication.Command.SendVerificationCode;
using Taskly_Application.Requests.Authentication.Command.VerificateEmail;
using Taskly_Application.Requests.Authentication.Command.Register;
using Taskly_Application.Requests.Authentication.Query.Login;
using Taskly_Application.Requests.Authentication.Query.GetAllAvatars;
using Taskly_Api.Response.Authenticate;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Taskly_Application.Requests.Authentication.Query.SendRequestToChangePassword;

namespace Taskly_Api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController(ISender sender, IMapper mapper) : ApiController
    {
        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] SendVerificationCodeRequest sendVerificationCodeRequest)
        {
            var result = await sender.Send(mapper.Map<SendVerificationCodeCommand>(sendVerificationCodeRequest));

            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
        [HttpPost("verificate-email")]
        public async Task<IActionResult> VerificateEmail([FromBody] VerificateEmailRequest verificateEmailRequest)
        {
            var result = await sender.Send(mapper.Map<VerificateEmailCommand>(verificateEmailRequest));

            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var result = await sender.Send(mapper.Map<RegisterCommand>(registerRequest));

            return result.Match(result => {
                Response.Cookies.Append("X-JWT-Token", result, new CookieOptions()
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });
                return Ok();
            },
                errors => Problem(errors));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await sender.Send(mapper.Map<LoginQuery>(loginRequest));
            
            return result.Match(result => {
                Response.Cookies.Append("X-JWT-Token", result, new CookieOptions()
                {
                    HttpOnly = true, // Забороняє взаємодіяти з кукі через JS
                    SameSite = SameSiteMode.Strict, //Кукі передаються тільки в межах того ж сайту (тільки для запитів з цього ж домену).
                }); 
                return Ok();
            },errors => Problem(errors));
        }
        [HttpGet("get-all-avatars")]
        public async Task<IActionResult> GetAllAvatars()
        {
            var result = await sender.Send(new GetAllAvatarsQuery());

            return result.Match(result => Ok(mapper.Map<AvatarResponse[]>(result.ToArray())),
                errors => Problem(errors));
        }

        
        [HttpGet("check-token")]
        [Authorize]
        public IActionResult CheckToken()
        {
            return Ok();
        }

        [HttpPost("send-request-to-change-password")]
        public async Task<IActionResult> SendRequestToChangePassword([FromBody] string Email)
        {
            var result = await sender.Send(new SendRequestToChangePasswordQuery(Email));
            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
    }
}
