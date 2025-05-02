using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.SetUserNameForSolanaUser;

public class SetUserNameForSolanaUserCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<SetUserNameForSolanaUserCommand, ErrorOr<SetUserNameForSolanaUserResult>>
{
    public async Task<ErrorOr<SetUserNameForSolanaUserResult>> Handle(SetUserNameForSolanaUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.Authentication.SetUserNameForSolanaUserAsync(request.PublicKey, request.UserName);
            
            return result.Match<ErrorOr<SetUserNameForSolanaUserResult>>(
                success => new SetUserNameForSolanaUserResult(success.UserName),
                error => error
            );
        }
        catch (Exception ex)
        {
            return Error.Unexpected("SetUserNameForSolanaUser", ex.Message);
        }
    }
}