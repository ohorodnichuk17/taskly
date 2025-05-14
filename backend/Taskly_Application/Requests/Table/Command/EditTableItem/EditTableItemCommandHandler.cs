using MediatR;
using ErrorOr;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.EditTableItem;

public class EditTableItemCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<EditTableItemCommand, ErrorOr<TableItemEntity>>
{
    public async Task<ErrorOr<TableItemEntity>> Handle(EditTableItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var editedTableItem = await unitOfWork.TableItems.EditTableItemAsync(request.Id, request.Task, 
                request.Status, request.EndTime, request.Label);
            await unitOfWork.SaveChangesAsync("cancellationToken");
            return editedTableItem;
        }
        catch (Exception ex)
        {
            return Error.Failure("Edit Table Item Error", ex.Message);
        }
    }
}