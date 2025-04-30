using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.UpdateUserProfile;

public record UpdateUserProfileCommand(
    string PublicKey,
    Guid AvatarId,
    string Username) : IRequest<ErrorOr<UpdateUserProfileResult>>;