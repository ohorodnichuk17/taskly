using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Challenge.Command.Delete;

public record DeleteChallengedCommand(
    Guid Id) : IRequest<ErrorOr<bool>>;