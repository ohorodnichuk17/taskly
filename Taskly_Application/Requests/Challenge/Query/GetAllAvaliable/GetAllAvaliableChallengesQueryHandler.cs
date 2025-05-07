using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;
using ErrorOr;

namespace Taskly_Application.Requests.Challenge.Query.GetAllAvaliable;

public class GetAllAvaliableChallengesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllAvaliableChallengesQuery, ErrorOr<IEnumerable<ChallengeEntity>>>
{
    public async Task<ErrorOr<IEnumerable<ChallengeEntity>>> Handle(GetAllAvaliableChallengesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var challenges = (await unitOfWork.Challenges.GetAvailableChallengesAsync()).ToList();
            
            if (!challenges.Any())
            {
                return Error.NotFound("No available challenges found.");
            }

            return challenges;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetAllAvaliableChallengesError", ex.Message);
        }
    }
}