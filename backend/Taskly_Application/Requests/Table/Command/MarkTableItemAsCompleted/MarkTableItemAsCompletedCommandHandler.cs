using Taskly_Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Table.Command.MarkTableItemAsCompleted;

public class MarkTableItemAsCompletedCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MarkTableItemAsCompletedCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(MarkTableItemAsCompletedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.TableItems.MarkAsCompletedAsync(request.Id, request.IsCompleted);
            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure("Mark Table Item As Completed Error", ex.Message);
        }
    }
}