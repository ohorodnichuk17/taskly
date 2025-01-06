using ErrorOr;
using Mapster;
using MediatR;
using Taskly_Application.DTO;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Board.Query.GetTemplateBoardById;

public class GetTemplateBoardByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTemplateBoardByIdQuery, ErrorOr<TemplateBoardDto>>
{
    public async Task<ErrorOr<TemplateBoardDto>> Handle(GetTemplateBoardByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.Board.GetTemplateBoardAsync(request.Id);

            var dto = result.Adapt<TemplateBoardDto>();

            return dto;
        }
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        }
    }
}