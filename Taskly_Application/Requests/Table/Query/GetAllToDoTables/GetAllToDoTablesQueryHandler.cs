using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetAllToDoTables;

public class GetAllToDoTablesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllToDoTablesQuery, ErrorOr<IEnumerable<ToDoTableEntity>>>
{
    public async Task<ErrorOr<IEnumerable<ToDoTableEntity>>> Handle(GetAllToDoTablesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var toDoTables = await unitOfWork.ToDoTable.GetAllAsync();
            return toDoTables.ToErrorOr();
        }
        catch (Exception ex)
        {
            return Error.Failure("Error while getting ToDo tables", ex.Message);
        }
    }
}