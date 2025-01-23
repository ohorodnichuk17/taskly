using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Command.Create;

public class CreateBoardCommandHandler(IUnitOfWork unitOfWork, 
    ICurrentUserService currentUserService) 
    : IRequestHandler<CreateBoardCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var boardTemplate = await unitOfWork.BoardTemplates.GetByIdAsync(request.BoardTemplateId);
            if (boardTemplate == null)
                return Error.NotFound("Board template not found.");

            var cardLists = new[]
            {
                new CardListEntity { Title = "To-Do" },
                new CardListEntity { Title = "Doing" },
                new CardListEntity { Title = "Done" }
            };

            var board = new BoardEntity
            {
                Name = request.Name,
                Tag = request.Tag,
                IsTeamBoard = request.IsTeamBoard,
                BoardTemplateId =  boardTemplate.Id,
                CardLists = cardLists
            };
            
            var result = await unitOfWork.Board.CreateAsync(board);

            return result;
        }
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        }
    }
}