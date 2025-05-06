using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Command.Register;

public class RegisterCommandHandler(
    IUnitOfWork unitOfWork,
    IJwtService jwtService) : IRequestHandler<RegisterCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var avatar = await unitOfWork.Avatar.GetByIdAsync(request.AvatarId);

        if (avatar == null)
            return Error.NotFound("Avatar not found");
        
        var referralCode = await unitOfWork.Authentication.GenerateReferralCode();

        var newUser = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = request.Email,
            AvatarId = avatar.Id,
            ReferralCode = referralCode
        };

        var result = await unitOfWork.Authentication.CreateNewUser(newUser, request.Password);

        if (result.IsError)
            return result.FirstError;

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
                RegisteredUserId = newUser.Id,
                CreatedAt = DateTime.UtcNow
            };

            await unitOfWork.Invites.CreateAsync(invite);
        }

        var token = jwtService.GetJwtToken(result.Value, false);

        return token;
    }
}
