using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Command.Register;

public class RegisterCommandHandler(
    IAuthenticationRepository authenticationRepository,
    IAvatarRepository avatarRepository,
    IJwtService jwtService) : IRequestHandler<RegisterCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var avatar = await avatarRepository.GetAvatarById(request.AvatarId);

        if (avatar == null)
            return Error.NotFound("Avatar not found");

        var newUser = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = request.Email,
            Avatar = avatar
        };

        await authenticationRepository.CreateNewUser(newUser, request.Password);

        var token = jwtService.GetJwtToken(newUser);

        return token;
    }
}
