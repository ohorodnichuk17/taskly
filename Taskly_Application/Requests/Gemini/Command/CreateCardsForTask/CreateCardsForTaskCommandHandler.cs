using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain;

namespace Taskly_Application.Requests.Gemini.Command.CreateCardsForTask;

public class CreateCardsForTaskCommandHandler(IGeminiApiClient gemini, IUnitOfWork unitOfWork) : IRequestHandler<CreateCardsForTaskCommand, ErrorOr<List<string>>>
{
    public async Task<ErrorOr<List<string>>> Handle(CreateCardsForTaskCommand request, CancellationToken cancellationToken)
    {
        var cards = await gemini.CreateCardsForTask(request.Task);
        var cardListId = await unitOfWork.Board.GetIdOfCardsListByTitleAsync(request.BoardId, Constants.Todo);
        await unitOfWork.Board.CreateCardAsync(cards, Constants.Todo, cardListId, request.UserId);

        return cards;
    }
}
