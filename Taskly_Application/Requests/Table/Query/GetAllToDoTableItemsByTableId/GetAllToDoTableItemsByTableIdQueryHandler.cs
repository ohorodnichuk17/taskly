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
		try
		{
            var result = await unitOfWork.ToDoTable.GetByIdAsync(request.ToDoTableId);

            return result.ToDoItems.ToErrorOr();
		}
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        }
    }
}
