using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Gemini.Command.GenerateDeadlineSuggestions;

public class GenerateDeadlineSuggestionsCommandHandler(IGeminiApiClient apiClient)
    : IRequestHandler<GenerateDeadlineSuggestionsCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(GenerateDeadlineSuggestionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await apiClient.GenerateDeadlineSuggestionsAsync(request.TaskDescription);
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
    }
}