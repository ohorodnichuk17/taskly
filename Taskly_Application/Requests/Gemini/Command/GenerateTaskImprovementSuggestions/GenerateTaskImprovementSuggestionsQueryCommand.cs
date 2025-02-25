using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Gemini.Command.GenerateTaskImprovementSuggestions;

public class GenerateTaskImprovementSuggestionsQueryCommand(IGeminiApiClient apiClient) : IRequestHandler<GenerateTaskImprovementSuggestionsCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(GenerateTaskImprovementSuggestionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var taskImprovementSuggestions = await apiClient.GenerateTaskImprovementSuggestionsAsync(request.TaskDescription);
            return taskImprovementSuggestions;
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
    }
}