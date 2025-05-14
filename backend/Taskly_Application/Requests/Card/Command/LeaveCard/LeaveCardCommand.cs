using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Card.Command.LeaveCard;

public record LeaveCardCommand(Guid CardId) : IRequest<ErrorOr<Guid>>;
