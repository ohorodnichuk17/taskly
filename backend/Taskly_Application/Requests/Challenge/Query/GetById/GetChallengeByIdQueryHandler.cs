using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Challenge.Query.GetById;

public class GetChallengeByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetChallengeByIdQuery, ErrorOr<ChallengeEntity>>
{
    public async Task<ErrorOr<ChallengeEntity>> Handle(GetChallengeByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var challenge = await unitOfWork.Challenges.GetChallengeByIdAsync(request.Id);

            if (challenge is null)
                return Error.NotFound("Challenge.NotFound", "Challenge with the specified ID was not found.");

            return challenge;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetChallengeByIdError", ex.Message);
        }
    }
}