using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Card.Command.TransferCardToAnotherCardList;

public class TransferCardToAnotherCardListCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<TransferCardToAnotherCardListCommand, ErrorOr<AchievementEntity[]>>
{
    public async Task<ErrorOr<AchievementEntity[]>> Handle(TransferCardToAnotherCardListCommand request, CancellationToken cancellationToken)
    {
        var transferedCard = await unitOfWork.Cards.TransferCardToAnotherCardListAsync(request.ToCardListId, request.CardId);

        if (transferedCard == null)
            return Error.Conflict("Something went wrong.");

        var userOfCard = await unitOfWork.Authentication.GetByIdAsync(transferedCard.Id);

        if(userOfCard == null)
            return Error.NotFound("User is not found.");

        var achievements = new List<AchievementEntity>();
        if(userOfCard.PublicKey != null)
        {
            achievements = (await unitOfWork.Achievements.CompleateAchievementAsync(userOfCard)).ToList();
        }

        return achievements.ToArray();
    }
}
