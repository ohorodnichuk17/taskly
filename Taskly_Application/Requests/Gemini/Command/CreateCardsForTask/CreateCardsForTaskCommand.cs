using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Gemini.Command.CreateCardsForTask;

public record CreateCardsForTaskCommand(Guid BoardId, string Task) : IRequest<ErrorOr<List<string>>>;
