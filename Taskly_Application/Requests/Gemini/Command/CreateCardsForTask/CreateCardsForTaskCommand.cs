using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Gemini.Command.CreateCardsForTask;

public record CreateCardsForTaskCommand(Guid UserId, Guid BoardId, string Task) : IRequest<ErrorOr<CardEntity[]>>;
