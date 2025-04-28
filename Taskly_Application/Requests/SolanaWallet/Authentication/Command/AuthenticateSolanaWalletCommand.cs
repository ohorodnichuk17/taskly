using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command;

public record AuthenticateSolanaWalletCommand(
    string PublicKey) : IRequest<ErrorOr<AuthenticationResult>>;