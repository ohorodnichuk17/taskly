using Microsoft.AspNetCore.Mvc;
using Taskly_Application.Gemini.PromptRequest;
using Taskly_Infrastructure.Services;

namespace Taskly_Api.Controllers;

[Route("api/gemini")]
[ApiController]
public class GeminiController(GeminiApiClient geminiApiClient) : ApiController
{
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateContent([FromBody] PromptRequest request)
    {
        try
        {
            string response = await geminiApiClient.GenerateContentAsync(request.Prompt);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}