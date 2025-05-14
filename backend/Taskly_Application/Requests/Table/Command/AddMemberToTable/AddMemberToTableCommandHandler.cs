using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Table.Command.AddMemberToTable;

public class AddMemberToTableCommandHandler(IUnitOfWork unitOfWork, IUserService userService)
    : IRequestHandler<AddMemberToTableCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(AddMemberToTableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetUserByEmailAsync(request.MemberEmail);
            await unitOfWork.Table.AddMemberToTableAsync(request.TableId, user.Id);
            await unitOfWork.SaveChangesAsync("Error adding member to the table.");
            return Unit.Value;
        }
        catch (Exception ex)
        {
            return Error.Failure("An error occurred while adding member to table", ex.Message);
        }
    }
}