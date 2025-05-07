using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Challenge.Command.Create;

public class CreateChallengeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateChallengeCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateChallengeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var challenge = new ChallengeEntity
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                TimeRange = new TimeRangeEntity()
                {
                    StartTime = request.StartTime,
                    EndTime = request.EndTime
                },
                Points = request.Points,
                IsBooked = false,
                IsCompleted = false,
                IsActive = request.IsActive,
                RuleKey = request.RuleKey,
                TargetAmount = request.TargetAmount,
            };
            
            var result = await unitOfWork.Challenges.CreateAsync(challenge);
            return result;
        }
        catch (Exception ex)
        {
            return Error.Failure("CreateChallengeError", ex.Message);
        }
    }
}