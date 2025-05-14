using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Gemini.Command.GenerateTaskImprovementSuggestions;

public record GenerateTaskImprovementSuggestionsCommand(string TaskDescription)
    : IRequest<ErrorOr<string>>;