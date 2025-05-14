using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Challenge.Query.GetAllActive;

public class GetAllActiveChallengesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllActiveChallengesQuery, ErrorOr<IEnumerable<ChallengeEntity>>>
{
    public async Task<ErrorOr<IEnumerable<ChallengeEntity>>> Handle(GetAllActiveChallengesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = (await unitOfWork.Challenges.GetAllActiveChallengesAsync()).ToList();
            
            if (!result.Any())
            {
                return Error.NotFound("No challenges found.");
            }
            
            return result;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetAllActiveChallengesError", ex.Message);
        }
    }
}