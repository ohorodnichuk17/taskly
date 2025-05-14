using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query.GetUserReferralCode;

public record GetUserReferralCodeQuery(
    Guid UserId) : IRequest<ErrorOr<string>>;