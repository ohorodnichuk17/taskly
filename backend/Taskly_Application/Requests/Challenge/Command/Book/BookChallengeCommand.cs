using MediatR;
using ErrorOr;

namespace Taskly_Application.Requests.Challenge.Command.Book;

public record BookChallengeCommand(
    Guid ChallengeId,
    Guid UserId) : IRequest<ErrorOr<bool>>;