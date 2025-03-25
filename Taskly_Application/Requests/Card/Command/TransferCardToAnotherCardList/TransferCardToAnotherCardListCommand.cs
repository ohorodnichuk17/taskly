using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Taskly_Application.Requests.Card.Command.TransferCardToAnotherCardList;

public record TransferCardToAnotherCardListCommand(Guid AnotherCardListId, Guid CardId) : IRequest<ErrorOr<Guid>>;
