using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query.GetUserPublicKey;

public record GetUserPublicKeyQuery(
    Guid UserId) : IRequest<ErrorOr<string>>;