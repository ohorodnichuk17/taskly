using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Card.Command.CreateCard;

public record CreateCardCommand(Guid CardListId, string Task, DateTime Deadline, Guid? UserId): IRequest<ErrorOr<Guid>>;
