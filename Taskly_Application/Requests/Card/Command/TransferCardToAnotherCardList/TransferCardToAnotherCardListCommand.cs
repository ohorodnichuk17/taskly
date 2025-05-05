using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Card.Command.TransferCardToAnotherCardList;

public record TransferCardToAnotherCardListCommand(Guid ToCardListId, Guid CardId) : IRequest<ErrorOr<AchievementEntity[]>>;
