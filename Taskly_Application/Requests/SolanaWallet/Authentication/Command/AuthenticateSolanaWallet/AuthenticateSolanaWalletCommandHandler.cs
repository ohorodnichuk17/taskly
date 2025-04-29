using ErrorOr;
using MediatR;
using Taskly_Domain.ValueObjects;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.AuthenticateSolanaWallet;

public class AuthenticateSolanaWalletCommandHandler : IRequestHandler<AuthenticateSolanaWalletCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(AuthenticateSolanaWalletCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var walletAddress = WalletAddress.Create(request.PublicKey);
            return new AuthenticationResult(walletAddress.Value);
        }
        catch (Exception ex)
        {
            return Error.Failure("SolanaAuthenticationFailed", ex.Message);
        }
    }
}