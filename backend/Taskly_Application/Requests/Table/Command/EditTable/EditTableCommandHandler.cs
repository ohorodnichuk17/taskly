using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;
using ErrorOr;

namespace Taskly_Application.Requests.Table.Command.EditTable;

public class EditTableCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<EditTableCommand, ErrorOr<TableEntity>>
{
    public async Task<ErrorOr<TableEntity>> Handle(EditTableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var editedTable = await unitOfWork.Table.EditTableAsync(request.TableId, request.TableName);
            return editedTable;
        }
        catch (Exception ex)
        {
            return Error.Failure("Edit  Table Error", ex.Message);
        }
    }
}