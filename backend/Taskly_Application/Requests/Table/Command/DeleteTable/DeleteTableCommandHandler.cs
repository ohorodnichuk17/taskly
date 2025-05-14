using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Table.Command.DeleteTable;

public class DeleteTableCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTableCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.Table.DeleteTableAsync(request.TableId);
            if (!result)
                return Error.Failure("Failed to delete table");
            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure("Delete  Table Error", ex.Message);
        }
    }
}