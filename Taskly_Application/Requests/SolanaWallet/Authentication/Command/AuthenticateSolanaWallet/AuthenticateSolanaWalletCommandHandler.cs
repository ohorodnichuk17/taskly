using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain;
using Taskly_Domain.Entities;
using Taskly_Domain.ValueObjects;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.AuthenticateSolanaWallet;

public class AuthenticateSolanaWalletCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AuthenticateSolanaWalletCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(AuthenticateSolanaWalletCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var walletAddress = WalletAddress.Create(request.PublicKey);
            var existingUser = await unitOfWork.Authentication.GetUserByPublicKey(request.PublicKey);
            UserEntity user;
            if (existingUser is null)
            {
                user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    PublicKey = walletAddress.Value,
                    AvatarId = Constants.DefaultAvatarId
                };
                await unitOfWork.Authentication.CreateAsync(user);
                await unitOfWork.SaveChangesAsync("Create user profile error ");
            }
            else
                user = existingUser;
            return new AuthenticationResult(walletAddress.Value, user.Id);
        }
        catch (Exception ex)
        {
            return Error.Failure("SolanaAuthenticationFailed", ex.Message);
        }
    }
}