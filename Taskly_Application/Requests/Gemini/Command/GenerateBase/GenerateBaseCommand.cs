using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Gemini.Command.GenerateBase;

public record GenerateBaseCommand(string prompt) 
    : IRequest<ErrorOr<string>>;