using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.CardComment.Query.GetCommentsByCardId;

public class GetCommentsByCardIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCommentsByCardIdQuery, ErrorOr<CardCommentEntity[]>>
{
    public async Task<ErrorOr<CardCommentEntity[]>> Handle(GetCommentsByCardIdQuery request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.CardComments.GetCommentsByCardIdAsync(request.CardId);
        if (result == null)
            return Error.NotFound("Card isn't found");

        return result;
    }
}
