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
using Taskly_Application.Requests.Authentication.Query.CheckHasUserSentRequestToChangePassword;
using Taskly_Application.Requests.Authentication.Command.SendRequestToChangePassword;
using Taskly_Application.Requests.Authentication.Command.ChangePassword;
using Taskly_Application.Requests.Authentication.Query.GetInformationAboutUser;
using Taskly_Application.Requests.Authentication.Command.EditUserProfile;
using Taskly_Application.Requests.SolanaWallet.Authentication.Command.AuthenticateSolanaWallet;
using Taskly_Application.Requests.SolanaWallet.Authentication.Command.UpdateUserProfile;
using Taskly_Application.Requests.SolanaWallet.Authentication.Query.GenerateJwtToken;

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
        public async Task<IActionResult> Register([FromBody] Request.Authenticate.RegisterRequest registerRequest)
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
        public async Task<IActionResult> Login([FromBody] Request.Authenticate.LoginRequest loginRequest)
        {
            var result = await sender.Send(mapper.Map<LoginQuery>(loginRequest));
            

            return await result.MatchAsync(async result => {
                Response.Cookies.Append("X-JWT-Token", result, new CookieOptions()
                {
                    HttpOnly = true, // Забороняє взаємодіяти з кукі через JS
                    SameSite = SameSiteMode.Strict, //Кукі передаються тільки в межах того ж сайту (тільки для запитів з цього ж домену).
                });

                var user = await sender.Send(new GetInformationAboutUserQuery(loginRequest.Email));

                
                return user.Match(user => Ok(mapper.Map<InformationAboutUserResponse>(user)),
                    errors => Problem(errors));
            },errors => Task.FromResult(Problem(errors)));
        }
        [HttpGet("get-all-avatars")]
        public async Task<IActionResult> GetAllAvatars()
        {
            var result = await sender.Send(new GetAllAvatarsQuery());

            return result.Match(result => Ok(mapper.Map<AvatarResponse[]>(result.ToArray())),
                errors => Problem(errors));
        }

        [HttpPut("edit-avatar")]
        [Authorize]
        public async Task<IActionResult> EditAvatar([FromBody] EditAvatarRequest request)
        {
            var result = await sender.Send(mapper.Map<EditAvatarCommand>(request));

            return result.Match(result => Ok(mapper.Map<EditAvatarResponse>(result)),
                errors => Problem(errors));
        }
        
        [HttpGet("check-token")]
        [Authorize]
        public async Task<IActionResult> CheckToken()
        {
            var userEmail = User.Claims.First((c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")).Value;

            var user = await sender.Send(new GetInformationAboutUserQuery(userEmail));

            return user.Match(user => Ok(mapper.Map<InformationAboutUserResponse>(user)),
                errors=>Problem(errors));
        }

        [HttpPost("send-request-to-change-password")]
        public async Task<IActionResult> SendRequestToChangePassword([FromBody] EmailRequest EmailRequest)
        {
            var result = await sender.Send(new SendRequestToChangePasswordCommand(EmailRequest.Email));
            return result.Match(result => Ok(),
                errors => Problem(errors));
        }

        [HttpGet("check-has-user-sent-request-to-change-password")]
        public async Task<IActionResult> CheckHasUserSentRequestToChangePassword([FromQuery] CheckHasUserSentRequestToChangePasswordRequest checkHasUserSent)
        {
            var result = await sender.Send(mapper.Map<CheckHasUserSentRequestToChangePasswordQuery>(checkHasUserSent));

            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var result = await sender.Send(mapper.Map<ChangePasswordCommand>(changePasswordRequest));

            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
        
        [HttpPost("solana-auth")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateSolanaWalletCommand request)
        {
            try
            {
                var authResult = await sender.Send(request);

                if (authResult.IsError)
                    return BadRequest(new { Message = authResult.Errors.First().Description });

                var tokenQuery = new GenerateJwtTokenQuery(authResult.Value.PublicKey);
                var tokenResult = await sender.Send(tokenQuery);

                if (tokenResult.IsError)
                    return StatusCode(500, new { Message = tokenResult.Errors.First().Description });

                return Ok(new { Token = tokenResult.Value });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
        
        [HttpPut("update-solana-profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileForSolana updateUserProfileRequest)
        {
            var result = await sender.Send(mapper.Map<UpdateUserProfileCommand>(updateUserProfileRequest));

            return result.Match(result => Ok(mapper.Map<UpdateUserProfileForSolana>(result)),
                errors => Problem(errors));
        }

        [HttpGet("exit")]
        [Authorize]
        public async Task<IActionResult> Exit()
        {
            Response.Cookies.Delete("X-JWT-Token");
            return Ok();
        }
    }
}
