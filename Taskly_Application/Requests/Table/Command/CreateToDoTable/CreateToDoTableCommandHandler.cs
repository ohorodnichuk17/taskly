using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.CreateToDoTable;

public class CreateToDoTableCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateToDoTableCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateToDoTableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await unitOfWork.Authentication.GetByIdAsync(request.UserId);
            if (user == null)
                return Error.Conflict("User isn't exist");

            var newTable = new ToDoTableEntity() { Id = Guid.NewGuid() };
            await unitOfWork.ToDoTable.CreateAsync(newTable);

            if (newTable.Members == null)
                newTable.Members = new List<UserEntity>();
            newTable.Members.Add(user);
            await unitOfWork.ToDoTable.SaveAsync(newTable);
            if (user.ToDoTables == null)
                user.ToDoTables = new List<ToDoTableEntity>();
            user.ToDoTables.Add(newTable);

            
            await unitOfWork.Authentication.SaveAsync(user);
            
            return newTable.Id;
        }
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        }
        

    }
}
