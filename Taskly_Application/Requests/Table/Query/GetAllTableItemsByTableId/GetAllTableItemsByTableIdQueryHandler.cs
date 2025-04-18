using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetAllTableItemsByTableId;

public class GetAllTableItemsByTableIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllTableItemsByTableIdQuery, ErrorOr<ICollection<TableItemEntity>>>
{
    public async Task<ErrorOr<ICollection<TableItemEntity>>> Handle(GetAllTableItemsByTableIdQuery request, CancellationToken cancellationToken)
    {
            var result = await unitOfWork.Table.GetTableIncludeById(request.TableId);

           if(result == null)
                return Error.Conflict("Table not found");

            return result.ToDoItems.ToErrorOr();
    }
}
