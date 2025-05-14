using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Response.Achievement;
using Taskly_Application.Requests.Achievements.Query.GetAllAchievementsByUser;
using Taskly_Domain;

namespace Taskly_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController(ISender sender, IMapper mapper) : ControllerBase
    {
        [Authorize]
        [HttpGet("get-all-achievements-by-user")]
        public async Task<IActionResult> GetAllAchievementsByUser()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")!.Value;
            var achievements = await sender.Send(new GetAllAchievementsByUserQuery());

            return achievements.Match(achievements => Ok(achievements.Select(a => mapper.Map<AchievementResponse>((a,Guid.Parse(userId)))).ToArray()),
                errors => Problem(errors.ToArray()[0].Description));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.AdminRole)]
        [HttpGet("test")]
        public IActionResult TestMethod()
        {
            return Ok("ADMIN!");
        }
    }
}
