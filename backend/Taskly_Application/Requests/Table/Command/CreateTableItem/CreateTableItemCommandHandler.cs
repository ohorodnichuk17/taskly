using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.CreateTableItem;

public class CreateTableItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTableItemCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(CreateTableItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newTableItem = new TableItemEntity()
            {
                Id = Guid.NewGuid(),
                Text = request.Task,
                TimeRange = new TimeRangeEntity() { StartTime = DateTime.UtcNow, EndTime = request.EndTime },
                Status = request.Status,
                Label = request.Label,
                IsCompleted = request.IsCompleted,
                ToDoTableId = request.TableId
            };

            await unitOfWork.TableItems.CreateAsync(newTableItem);
            foreach (var userId in request.Members)
            {
                var user = await unitOfWork.Authentication.GetByIdAsync(userId);
                
                user.ToDoTableItems.Add(newTableItem);
                await unitOfWork.Authentication.SaveAsync(user);
            }

            return newTableItem.Id.ToString();
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
    }
}
