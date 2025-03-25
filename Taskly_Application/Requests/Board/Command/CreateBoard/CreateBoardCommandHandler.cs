using ErrorOr;
using MediatR;
using System.Reflection.Metadata;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Command.CreateBoard;

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
                new CardListEntity { Title = Constants.Todo },
                new CardListEntity { Title = Constants.Inprogress },
                new CardListEntity { Title = Constants.Todo }
            };

            var board = new BoardEntity
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Tag = request.Tag,
                IsTeamBoard = request.IsTeamBoard,
                BoardTemplateId =  boardTemplate.Id,
                CardLists = cardLists
            };
            
            var createdBoard = await unitOfWork.Board.CreateAsync(board);

            
            await unitOfWork.Board.AddMemberToBoardAsync(createdBoard, request.UserId);

            return createdBoard;
        }
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        }
    }
}