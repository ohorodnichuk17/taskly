using ErrorOr;
using Mapster;
using MediatR;
using Taskly_Application.DTO;
using Taskly_Application.DTO.TemplateBoardDTOs;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Board.Query.GetTemplateBoard;

public class GetTemplateBoardQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTemplateBoardQuery, ErrorOr<TemplateBoardDto>>
{
    public async Task<ErrorOr<TemplateBoardDto>> Handle(GetTemplateBoardQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.Board.GetTemplateBoardAsync();

            var dto = result.Adapt<TemplateBoardDto>();

            return dto;
        }
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        }
    }
}