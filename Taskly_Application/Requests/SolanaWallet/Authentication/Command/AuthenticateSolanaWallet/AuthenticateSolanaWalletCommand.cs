using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.AuthenticateSolanaWallet;

public record AuthenticateSolanaWalletCommand(
    string PublicKey, string? ReferralCode) : IRequest<ErrorOr<AuthenticationResult>>;