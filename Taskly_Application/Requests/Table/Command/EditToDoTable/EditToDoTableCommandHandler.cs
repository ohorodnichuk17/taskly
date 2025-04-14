using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;
using ErrorOr;

namespace Taskly_Application.Requests.Table.Command.EditToDoTable;

public class EditToDoTableCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<EditToDoTableCommand, ErrorOr<ToDoTableEntity>>
{
    public async Task<ErrorOr<ToDoTableEntity>> Handle(EditToDoTableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var editedTable = await unitOfWork.ToDoTable.EditToDoTableAsync(request.TableId, request.TableName);
            return editedTable;
        }
        catch (Exception ex)
        {
            return Error.Failure("Edit ToDo Table Error", ex.Message);
        }
    }
}