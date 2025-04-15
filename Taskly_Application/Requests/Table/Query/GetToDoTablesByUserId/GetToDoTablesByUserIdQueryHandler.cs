using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetToDoTablesByUserId;

public class GetToDoTablesByUserIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetToDoTablesByUserIdQuery, ErrorOr<ICollection<ToDoTableEntity>>>
{
    public async Task<ErrorOr<ICollection<ToDoTableEntity>>> Handle(GetToDoTablesByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.ToDoTable.GetToDoTablesByUserIdAsync(request.UserId);

            return result.ToErrorOr();
        }
        catch (Exception ex)
        {
            return Error.Failure("Get To Do Tables By User Id Error", ex.Message);
        }
    }
}