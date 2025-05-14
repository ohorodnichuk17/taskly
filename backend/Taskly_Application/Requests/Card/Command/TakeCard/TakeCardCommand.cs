using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Card.Command.TakeCard;

public record TakeCardCommand(Guid CardId,Guid UserId) : IRequest<ErrorOr<Guid>>;
