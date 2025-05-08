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
                var referralCode = await unitOfWork.Authentication.GenerateReferralCode();
                user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    PublicKey = walletAddress.Value,
                    AvatarId = Constants.DefaultAvatarId,
                    ReferralCode = referralCode,
                };
                await unitOfWork.Authentication.CreateAsync(user);
                await unitOfWork.Achievements.ChangePercentageOfCompletionOfAllAchievements();
                var userLevel = new UserLevelEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    DataAchieved = DateTime.UtcNow,
                    Level = 1,
                    CompletedTasks = 0
                };
                await unitOfWork.UserLevels.CreateAsync(userLevel);
                
                if (!string.IsNullOrEmpty(request.ReferralCode))
                {
                    var inviter = await unitOfWork.Authentication.GetUserByReferralCodeAsync(request.ReferralCode);
                    if (inviter == null)
                    {
                        return Error.Validation("Invalid referral code.");
                    }
                    var invite = new InviteEntity
                    {
                        Id = Guid.NewGuid(),
                        InvitedByUserId = inviter.Id,
                        RegisteredUserId = user.Id,
                        CreatedAt = DateTime.UtcNow
                    };
                    await unitOfWork.Invites.CreateAsync(invite);
                }
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