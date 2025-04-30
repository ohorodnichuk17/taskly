using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.UpdateUserProfile;

public class UpdateUserProfileCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserProfileCommand, ErrorOr<UpdateUserProfileResult>>
{
    public async Task<ErrorOr<UpdateUserProfileResult>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.Authentication.UpdateSolanaUserProfileAsync(
                request.PublicKey, request.AvatarId, request.Username);
            return new UpdateUserProfileResult(request.PublicKey, result.AvatarId, result.UserName);
        }
        catch (Exception ex)
        {
            return Error.Failure("Create user profile failed: ", ex.Message);
        }
    }
}