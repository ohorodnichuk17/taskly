using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.CardComment.Command.LeaveCardComment;

public record LeaveCardCommentCommand(Guid CardId, Guid UserId, string Text) : IRequest<ErrorOr<Guid>>;
