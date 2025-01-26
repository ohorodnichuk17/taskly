using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.CreateToDoTableItem;

public class CreateToDoTableItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateToDoTableItemCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(CreateToDoTableItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newTableItem = new ToDoItemEntity()
            {
                Id = Guid.NewGuid(),
                Text = request.Task,
                TimeRange = new TimeRangeEntity() { StartTime = DateTime.UtcNow, EndTime = request.EndTime },
                Status = request.Status,
                Label = request.Label,
                ToDoTableId = request.ToDoTableId
            };

            await unitOfWork.ToDoTableItems.CreateAsync(newTableItem);
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
            return Error.Conflict(ex.Message);
        }
        
    }
}
