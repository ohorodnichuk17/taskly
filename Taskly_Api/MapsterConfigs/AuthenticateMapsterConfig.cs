using Mapster;
using Taskly_Api.Request.Authenticate;
using Taskly_Application.Requests.Authentication.Command.Register;
using Taskly_Application.Requests.Authentication.Command.SendVerificationEmail;
using Taskly_Application.Requests.Authentication.Command.VerificateEmail;
using Taskly_Application.Requests.Authentication.Query.Login;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public static class AuthenticateMapsterConfig
{
    public static void Config()
    {
        TypeAdapterConfig<SendVerificationCodeRequest, SendVerificationCodeCommand>.NewConfig()
            .Map(src => src.Email, desp => desp.Email);

        TypeAdapterConfig<VerificateEmailRequest, VerificateEmailCommand>.NewConfig()
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Code, desp => desp.Code);

        TypeAdapterConfig<RegisterRequest, RegisterCommand>.NewConfig()
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Password, desp => desp.Password)
            .Map(src => src.ConfirmPassword, desp => desp.ConfirmPassword)
            .Map(src => src.AvatarId, desp => desp.AvatarId);

        TypeAdapterConfig<LoginRequest, LoginQuery>.NewConfig()
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Password, desp => desp.Password);

        TypeAdapterConfig<UserForTableItemResponse, UserEntity>.NewConfig()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Avatar.ImagePath, desp => desp.Avatar);
    }
}
