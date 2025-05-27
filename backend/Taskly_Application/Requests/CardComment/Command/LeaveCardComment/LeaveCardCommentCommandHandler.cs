using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.CardComment.Command.LeaveCardComment;

public class LeaveCardCommentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<LeaveCardCommentCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(LeaveCardCommentCommand request, CancellationToken cancellationToken)
    {
        var commentId = await unitOfWork.CardComments.LeaveCommentAsync(request.CardId, request.UserId, request.Text);

        if (commentId == null)
            return Error.Conflict("Something went wrong");

        return commentId.Value;
    }
}
