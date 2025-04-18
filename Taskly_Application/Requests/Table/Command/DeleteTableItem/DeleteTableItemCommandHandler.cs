using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Table.Command.DeleteTableItem;

public class DeleteTableItemCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTableItemCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(DeleteTableItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.TableItems.DeleteAsync(request.Id);
            if (!result)
                return Error.Failure("Failed to delete table item");
            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure("Delete Table Item Error", ex.Message);
        }
    }
}