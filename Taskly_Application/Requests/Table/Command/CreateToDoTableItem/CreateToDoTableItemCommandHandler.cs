using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.CreateToDoTableItem;

public class CreateToDoTableItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateToDoTableItemCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(CreateToDoTableItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var users = new List<UserEntity>();
            foreach (var userId in request.Members)
            {
                users.Add(await unitOfWork.Authentication.GetByIdAsync(userId));
            }

            var newTableItem = new ToDoItemEntity()
            {
                Id = Guid.NewGuid(),
                Text = request.Task,
                TimeRange = new TimeRangeEntity() { EndTime = request.EndTime },
                Status = request.Status,
                Label = request.Label,
                Members = users,
                ToDoTableId = request.ToDoTableId
            };
            await unitOfWork.ToDoTableItems.CreateAsync(newTableItem);

            return newTableItem.Id.ToString();
        }
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        }
        
    }
}
