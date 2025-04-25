using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetTableById;

public class GetTableByIdQueryHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<GetTableByIdQuery, ErrorOr<TableEntity>>
{
    public async Task<ErrorOr<TableEntity>> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var table = await unitOfWork.Table.GetTableIncludeByIdAsync(request.TableId);
            if (table is null)
                return Error.NotFound(" Table not found");
            return table;
        }
        catch (Exception ex)
        {
            return Error.Failure("Get  Table Error", ex.Message);
        }
    }
}