using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query.GenerateJwtToken;

public record GenerateJwtTokenQuery(
    string PublicKey) : IRequest<ErrorOr<string>>;