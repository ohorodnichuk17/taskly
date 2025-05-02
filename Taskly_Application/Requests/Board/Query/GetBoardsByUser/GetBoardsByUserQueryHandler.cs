using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Query.GetBoardsByUser;

internal class GetBoardsByUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBoardsByUserQuery, ErrorOr<ICollection<BoardEntity>>>
{
    public async Task<ErrorOr<ICollection<BoardEntity>>> Handle(GetBoardsByUserQuery request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Board.GetBoardsByUserAsync(request.UserId);

        return result!.ToErrorOr();
    }
}
