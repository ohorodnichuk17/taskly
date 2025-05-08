using Taskly_Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.UserLevel.Query.GetByUserId;

public class GetUserLevelByUserIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetUserLevelByUserIdQuery, ErrorOr<int>>
{
    public async Task<ErrorOr<int>> Handle(GetUserLevelByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userLevel = await unitOfWork.UserLevels
                .GetLevelPropertyByUserIdAsync(request.UserId);

            return userLevel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}