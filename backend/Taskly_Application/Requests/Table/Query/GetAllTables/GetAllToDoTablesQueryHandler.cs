using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetAllTables;

public class GetAllTablesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllTablesQuery, ErrorOr<IEnumerable<TableEntity>>>
{
    public async Task<ErrorOr<IEnumerable<TableEntity>>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var toDoTables = await unitOfWork.Table.GetAllAsync();
            return toDoTables.ToErrorOr();
        }
        catch (Exception ex)
        {
            return Error.Failure("Error while getting  tables", ex.Message);
        }
    }
}