using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Gemini.Command.SummarizeText;

public record SummarizeTextCommand(string Text) 
    : IRequest<ErrorOr<string>>;