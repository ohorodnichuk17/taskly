using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Gemini.Command.CreateCardsForTask;

public class CreateCardsForTaskCommandHandler(IGeminiApiClient gemini, IUnitOfWork unitOfWork) : IRequestHandler<CreateCardsForTaskCommand, ErrorOr<CardEntity[]>>
{
    public async Task<ErrorOr<CardEntity[]>> Handle(CreateCardsForTaskCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"USER IDDDDDDDDDD - {request.UserId}");
        var cardsTitles = await gemini.CreateCardsForTask(request.Task);
        var isUserExist = await unitOfWork.Authentication.IsUserExist(request.UserId);
        if (isUserExist == false)
            return Error.NotFound("User is not found.");
        var cardListId = await unitOfWork.Board.GetIdOfCardsListByTitleAsync(request.BoardId, Constants.Todo);
        var cards = await unitOfWork.Cards.CreateCardAsync(cardsTitles, Constants.Todo, cardListId, request.UserId);

        return cards.ToErrorOr();
    }
}
