using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Table.Command.CreateTable;

public class CreateTableCommandHandler(IUnitOfWork unitOfWork, IUserService userService) : IRequestHandler<CreateTableCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetUserByIdAsync(request.UserId);
            var newTable = await unitOfWork.Table.CreateNewTableAsync(request.Name);
            var userFromTable = await unitOfWork.Table.AddNewUserToTableAsync(newTable,user);
            await unitOfWork.Authentication.SaveAsync(userFromTable);
            return newTable.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating table: {ex.Message}");
            return Error.Conflict(ex.Message);
        }
    }
}
