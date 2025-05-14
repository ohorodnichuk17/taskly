using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Gemini.Command.GenerateDeadlineSuggestions;

public record GenerateDeadlineSuggestionsCommand(string TaskDescription)
    : IRequest<ErrorOr<string>>;