using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query.GetUserPublicKey;

public class GetUserPublicKeyQueryHadler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetUserPublicKeyQuery, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(GetUserPublicKeyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var publicKey = await unitOfWork.Authentication.GetUserPublicKeyAsync(request.UserId);

            if (string.IsNullOrEmpty(publicKey))
                return Error.NotFound("GetUserPublicKeyError", "User not found");

            return publicKey;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetUserPublicKeyError", ex.Message);
        }
    }
}