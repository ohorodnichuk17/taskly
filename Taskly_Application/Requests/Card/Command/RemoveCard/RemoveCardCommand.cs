using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Card.Command.RemoveCard;

public record RemoveCardCommand(Guid CardId) : IRequest<Deleted>;

