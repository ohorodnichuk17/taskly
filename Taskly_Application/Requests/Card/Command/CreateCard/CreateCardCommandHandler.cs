using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Card.Command.CreateCard;

public class CreateCardCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<CreateCardCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Cards.CreateCardAsync(request.CardListId, request.Task, request.Deadline, request.UserId);

        if (result == null)
            return Error.Conflict("Something went wrong");

        return result.Value;
    }
}
