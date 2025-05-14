using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query.GetUserReferralCode;

public class GetUserReferralCodeQueryHadler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetUserReferralCodeQuery, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(GetUserReferralCodeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var referralCode = await unitOfWork.Authentication.GetUserReferralCodeAsync(request.UserId);

            if (string.IsNullOrEmpty(referralCode))
                return Error.NotFound("GetUserReferralCodeError", "User not found");

            return referralCode;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetUserReferralCodeError", ex.Message);
        }
    }
}