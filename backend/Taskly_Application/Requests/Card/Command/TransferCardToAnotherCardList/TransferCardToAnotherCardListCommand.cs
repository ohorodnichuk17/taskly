using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Card.Command.TransferCardToAnotherCardList;

public record TransferCardToAnotherCardListCommand(Guid ToCardListId, Guid CardId) : IRequest<ErrorOr<Guid>>;
