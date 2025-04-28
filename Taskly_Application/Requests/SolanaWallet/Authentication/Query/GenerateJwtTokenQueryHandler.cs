using Taskly_Application.Interfaces.IService;
using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query;

public class GenerateJwtTokenQueryHandler(IJwtService jwtService)
    : IRequestHandler<GenerateJwtTokenQuery, ErrorOr<string>>
{
    public Task<ErrorOr<string>> Handle(GenerateJwtTokenQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = jwtService.GetJwtToken(request.PublicKey, false).ToErrorOr();
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            return Task.FromResult<ErrorOr<string>>(Error.Conflict("JwtTokenForSolanaGenerationFailed", ex.Message));
        }
    }
}