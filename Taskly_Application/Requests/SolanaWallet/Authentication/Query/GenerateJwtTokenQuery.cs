using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query;

public record GenerateJwtTokenQuery(
    string PublicKey) : IRequest<ErrorOr<string>>;