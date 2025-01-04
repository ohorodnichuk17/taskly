using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetAllToDoTableItemsByTableId;

public class GetAllToDoTableItemsByTableIdQueryHandler(IRepository<ToDoTableEntity> repository) : IRequestHandler<GetAllToDoTableItemsByTableIdQuery, ErrorOr<ICollection<ToDoItemEntity>>>
{
    public async Task<ErrorOr<ICollection<ToDoItemEntity>>> Handle(GetAllToDoTableItemsByTableIdQuery request, CancellationToken cancellationToken)
    {
		try
		{
            var result = await repository.GetByIdAsync(request.ToDoTableId);

            return result.ToDoItems.ToErrorOr();
		}
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        }
    }
}
