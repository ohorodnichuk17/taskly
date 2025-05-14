using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Card.Query.GetCardsListsByBoardId;

public record GetCardListByBoardIdQuery(Guid BoardId, Guid UserId) : IRequest<ErrorOr<CardListEntity[]>>;
