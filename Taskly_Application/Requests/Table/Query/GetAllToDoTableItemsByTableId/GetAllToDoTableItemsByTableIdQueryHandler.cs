using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetAllToDoTableItemsByTableId;

public class GetAllToDoTableItemsByTableIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllToDoTableItemsByTableIdQuery, ErrorOr<ICollection<ToDoItemEntity>>>
{
    public async Task<ErrorOr<ICollection<ToDoItemEntity>>> Handle(GetAllToDoTableItemsByTableIdQuery request, CancellationToken cancellationToken)
    {
            var result = await unitOfWork.ToDoTable.GetToDoTableIncludeById(request.ToDoTableId);

           if(result == null)
                return Error.Conflict("Table not found");

            return result.ToDoItems.ToErrorOr();
    }
}
