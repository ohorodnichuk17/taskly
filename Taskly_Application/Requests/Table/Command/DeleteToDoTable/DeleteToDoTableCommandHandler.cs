using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Table.Command.DeleteToDoTable;

public class DeleteToDoTableCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteToDoTableCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(DeleteToDoTableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.ToDoTable.DeleteToDoTableAsync(request.TableId);
            if (!result)
                return Error.Failure("Failed to delete table");
            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure("Delete ToDo Table Error", ex.Message);
        }
    }
}