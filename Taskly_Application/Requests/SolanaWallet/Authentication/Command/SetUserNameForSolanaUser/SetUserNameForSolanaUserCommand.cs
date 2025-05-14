using MediatR;
using ErrorOr;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.SetUserNameForSolanaUser;

public record SetUserNameForSolanaUserCommand(
    string PublicKey,
    string UserName) : IRequest<ErrorOr<SetUserNameForSolanaUserResult>>;