using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Query.GetTemplatesOfBoard;

public class GetTemplatesOfBoardQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTemplatesOfBoardQuery, BoardTemplateEntity[]>
{
    public async Task<BoardTemplateEntity[]> Handle(GetTemplatesOfBoardQuery request, CancellationToken cancellationToken)
    {
        return (await unitOfWork.BoardTemplates.GetAllAsync()).ToArray();
    }
}
