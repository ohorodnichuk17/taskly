using ErrorOr;
using MediatR;
using Taskly_Domain.ValueObjects;

namespace Taskly_Application.Requests.Card.Command.ChangeCard;

public record ChangeCardCommand(Guid CardId, ChangeCardProps ChangeCardProps) : IRequest<ErrorOr<Updated>>;
