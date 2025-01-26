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

            var newTable = await unitOfWork.ToDoTable.CreateNewToDoTableAsync();

            var userFromToDoTable = await unitOfWork.ToDoTable.AddNewUserToToDoTableAsync(newTable,user);
            
            await unitOfWork.Authentication.SaveAsync(userFromToDoTable);
            
            return newTable.Id;
        }
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        }
        

    }
}
