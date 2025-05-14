using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Authentication.Query.Login;

public class LoginQueryHandler(
    IUnitOfWork unitOfWork, 
    IJwtService jwtService) : IRequestHandler<LoginQuery, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Authentication.GetUserByEmail(request.Email);

        if (user == null) return Error.Conflict("Invalid email or password");

        var isPasswordValid = await unitOfWork.Authentication.IsPasswordValid(user, request.Password);

        if(!isPasswordValid) return Error.Conflict("Invalid email or password");

        var token = jwtService.GetJwtToken(user, request.RememberMe);

        return token;
    }
}
