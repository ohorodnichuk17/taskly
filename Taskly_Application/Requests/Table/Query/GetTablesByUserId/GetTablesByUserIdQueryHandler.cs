using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetTablesByUserId;

public class GetTablesByUserIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTablesByUserIdQuery, ErrorOr<ICollection<TableEntity>>>
{
    public async Task<ErrorOr<ICollection<TableEntity>>> Handle(GetTablesByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.Table.GetTablesByUserIdAsync(request.UserId);

            return result.ToErrorOr();
        }
        catch (Exception ex)
        {
            return Error.Failure("Get To Do Tables By User Id Error", ex.Message);
        }
    }
}