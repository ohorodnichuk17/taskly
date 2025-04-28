using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Application.Requests.SolanaWallet.Authentication.Command;
using Taskly_Application.Requests.SolanaWallet.Authentication.Query;

namespace Taskly_Api.Controllers;

[Route("api/solana-auth")]
[ApiController]
public class SolanaAuthController(ISender sender) : ApiController
{
    [HttpPost("authenticate")]
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
}