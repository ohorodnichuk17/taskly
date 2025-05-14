using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query.GetRoleByUserId;

public class GetRoleByUserIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoleByUserIdQuery, string>
{
    public async Task<string> Handle(GetRoleByUserIdQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine("USEr id - ", request.UserId.ToString());

        var role = await unitOfWork.Authentication.GetUserRoleByIdAsync(request.UserId);

        return role;
    }
}
