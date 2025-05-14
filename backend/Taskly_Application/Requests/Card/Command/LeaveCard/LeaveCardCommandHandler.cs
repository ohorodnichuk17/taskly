using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Card.Command.LeaveCard;

public class LeaveCardCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<LeaveCardCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(LeaveCardCommand request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Cards.LeaveCardAsync(request.CardId);

        if (result == null)
            return Error.NotFound("Card is not found.");

        return result.Value;
    }
}
