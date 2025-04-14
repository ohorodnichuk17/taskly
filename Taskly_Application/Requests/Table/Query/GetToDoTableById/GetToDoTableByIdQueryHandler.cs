using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetToDoTableById;

public class GetToDoTableByIdQueryHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<GetToDoTableByIdQuery, ErrorOr<ToDoTableEntity>>
{
    public async Task<ErrorOr<ToDoTableEntity>> Handle(GetToDoTableByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var table = await unitOfWork.ToDoTable.GetToDoTableIncludeById(request.TableId);
            if (table is null)
                return Error.NotFound("ToDo Table not found");
            return table;
        }
        catch (Exception ex)
        {
            return Error.Failure("Get ToDo Table Error", ex.Message);
        }
    }
}