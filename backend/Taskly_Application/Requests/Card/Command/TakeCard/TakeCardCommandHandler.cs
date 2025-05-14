using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Card.Command.TakeCard;

public class TakeCardCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<TakeCardCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(TakeCardCommand request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Cards.TakeCardAsync(request.CardId, request.UserId);

        if (result == null)
            return Error.NotFound("Something is not found.");

        return result.Value;
    }
}
