using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Table.Command.RemoveMemberFromTable;

public class RemoveMemberFromTableCommandHandler(IUnitOfWork unitOfWork, IUserService userService)
    : IRequestHandler<RemoveMemberFromTableCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(RemoveMemberFromTableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetUserByEmailAsync(request.MemberEmail);
            await unitOfWork.Table.RemoveMemberFromTableAsync(request.TableId, user.Id);
            await unitOfWork.SaveChangesAsync("Error removing member from the table.");
            return Unit.Value;
        }
        catch (Exception ex)
        {
            return Error.Failure("An error occurred while removing member from table", ex.Message);
        }
    }
}