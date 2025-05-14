using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;
using ErrorOr;

namespace Taskly_Application.Requests.Challenge.Query.GetAll;

public class GetAllChallengesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllChallengesQuery, ErrorOr<IEnumerable<ChallengeEntity>>>
{
    public async Task<ErrorOr<IEnumerable<ChallengeEntity>>> Handle(GetAllChallengesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var challenges = (await unitOfWork.Challenges.GetAllChallengesAsync()).ToList();

            if (!challenges.Any())
            {
                return Error.NotFound("No challenges found.");
            }

            return challenges;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetAllChallengesError", ex.Message);
        }
    }
}