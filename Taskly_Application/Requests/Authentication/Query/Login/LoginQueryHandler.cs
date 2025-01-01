using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Authentication.Query.Login;

public class LoginQueryHandler(
    IAuthenticationRepository authenticationRepository, 
    IJwtService jwtService) : IRequestHandler<LoginQuery, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await authenticationRepository.GetUserByEmail(request.Email);

        if (user == null) return Error.Conflict("Invalid email or password");

        var isPasswordValid = await authenticationRepository.IsPasswordValid(user, request.Password);

        if(!isPasswordValid) return Error.Conflict("Invalid email or password");

        var token = jwtService.GetJwtToken(user);

        return token;
    }
}
