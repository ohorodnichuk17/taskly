using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Gemini;
using Taskly_Application.Gemini.PromptRequest;
using Taskly_Application.Requests.Gemini.Command.CreateCardsForTask;
using Taskly_Application.Requests.Gemini.Command.GenerateBase;
using Taskly_Application.Requests.Gemini.Command.GenerateDeadlineSuggestions;
using Taskly_Application.Requests.Gemini.Command.GenerateTaskImprovementSuggestions;
using Taskly_Application.Requests.Gemini.Command.TranslateText;

namespace Taskly_Api.Controllers;

[Route("api/gemini")]
[ApiController]
public class GeminiController(ISender sender, IMapper mapper) : ApiController
{
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateContent([FromBody] PromptRequest request)
    {
        try
        {
            var result = await sender.Send(
                new GenerateBaseCommand(request.Prompt));
            return result.Match(r => Ok(r),
                errors => Problem(errors));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("generate-task-improvement-suggestions")]
    public async Task<IActionResult> GenerateTaskImprovementSuggestions([FromBody] PromptRequest request)
    {
        try
        {
            var result = await sender.Send(
                new GenerateTaskImprovementSuggestionsCommand(request.Prompt));
            return result.Match(r => Ok(r),
                errors => Problem(errors));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("generate-deadline-suggestions")]
    public async Task<IActionResult> GenerateDeadlineSuggestions([FromBody] PromptRequest request)
    {
        try
        {
            var result = await sender.Send(
                new GenerateDeadlineSuggestionsCommand(request.Prompt));
            return result.Match(r => Ok(r),
                errors => Problem(errors));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("translate-text")]
    public async Task<IActionResult> Translate([FromBody] TranslateTextRequest request)
    {
        try
        {
            var result = await sender.Send(
                new TranslateTextCommand(request.SourceLanguage, request.TargetLanguage, request.Text));
            return result.Match(r => Ok(r),
                errors => Problem(errors));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("summarize")]
    public async Task<IActionResult> Summarize([FromBody] PromptRequest request)
    {
        try
        {
            var result = await sender.Send(
                new GenerateBaseCommand(request.Prompt));
            return result.Match(r => Ok(r),
                errors => Problem(errors));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize]
    [HttpPost("create-cards-for-task")]
    public async Task<IActionResult> CreateCardsForTask([FromBody] CreateCardsForTaskRequest createCardsForTaskRequest)
    {
        var result = await sender.Send(mapper.Map<CreateCardsForTaskCommand>(createCardsForTaskRequest));

        return result.Match(result => Ok(result),
        errors => Problem(errors));
    }
}