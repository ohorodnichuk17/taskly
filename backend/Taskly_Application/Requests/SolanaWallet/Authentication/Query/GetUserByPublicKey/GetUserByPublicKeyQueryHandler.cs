using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query.GetUserByPublicKey;

public class GetUserByPublicKeyQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetUserByPublicKeyQuery, ErrorOr<UserEntity>>
{
    public async Task<ErrorOr<UserEntity>> Handle(GetUserByPublicKeyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await unitOfWork.Authentication.GetUserByPublicKey(request.PublicKey);
            if(user == null)
                throw new Exception("User not found");
            return user;
        }
        catch (Exception ex)
        {
            return Error.Failure("Get user by public key failed: ", ex.Message);
        }
    }
}