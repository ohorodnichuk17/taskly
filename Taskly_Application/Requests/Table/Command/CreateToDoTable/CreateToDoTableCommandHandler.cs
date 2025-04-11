using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Table.Command.CreateToDoTable;

public class CreateToDoTableCommandHandler(IUnitOfWork unitOfWork, IUserService userService) : IRequestHandler<CreateToDoTableCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateToDoTableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetUserByIdAsync(request.UserId);
            var newTable = await unitOfWork.ToDoTable.CreateNewToDoTableAsync(request.Name);
            var userFromToDoTable = await unitOfWork.ToDoTable.AddNewUserToToDoTableAsync(newTable,user);
            await unitOfWork.Authentication.SaveAsync(userFromToDoTable);
            return newTable.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating table: {ex.Message}");
            return Error.Conflict(ex.Message);
        }
    }
}
