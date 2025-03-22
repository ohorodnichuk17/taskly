using Mapster;
using Taskly_Api.Request.Authenticate;
using Taskly_Api.Response.Authenticate;
using Taskly_Application.Requests.Authentication.Command.ChangePassword;
using Taskly_Application.Requests.Authentication.Command.Register;
using Taskly_Application.Requests.Authentication.Command.SendVerificationCode;
using Taskly_Application.Requests.Authentication.Command.VerificateEmail;
using Taskly_Application.Requests.Authentication.Query.CheckHasUserSentRequestToChangePassword;
using Taskly_Application.Requests.Authentication.Query.Login;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public class AuthenticateMapsterConfig : IRegister
{
    /*public static void Config()
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
            .Map(src => src.Password, desp => desp.Password)
            .Map(src => src.RememberMe, desp => desp.RememberMe);

        TypeAdapterConfig<UserEntity,UserForTableItemResponse>.NewConfig()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Avatar, desp => desp.Avatar.ImagePath);

        TypeAdapterConfig<AvatarEntity, AvatarResponse>.NewConfig()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Name, desp => desp.ImagePath);

        TypeAdapterConfig<CheckHasUserSentRequestToChangePasswordRequest, CheckHasUserSentRequestToChangePasswordQuery>.NewConfig()
            .Map(src => src.Key, desp => desp.Key);

        TypeAdapterConfig<ChangePasswordRequest, ChangePasswordCommand>.NewConfig()
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Password, desp => desp.Password)
            .Map(src => src.ConfirmPassword, desp => desp.ConfirmPassword);


        TypeAdapterConfig<UserEntity, InformationAboutUserResponse>.NewConfig()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.AvatarName, desp => desp.Avatar != null ? desp.Avatar.ImagePath : "");

    }*/

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SendVerificationCodeRequest, SendVerificationCodeCommand>()
            .Map(src => src.Email, desp => desp.Email);

        config.NewConfig<VerificateEmailRequest, VerificateEmailCommand>()
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Code, desp => desp.Code);

        config.NewConfig<RegisterRequest, RegisterCommand>()
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Password, desp => desp.Password)
            .Map(src => src.ConfirmPassword, desp => desp.ConfirmPassword)
            .Map(src => src.AvatarId, desp => desp.AvatarId);

        config.NewConfig<LoginRequest, LoginQuery>()
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Password, desp => desp.Password)
            .Map(src => src.RememberMe, desp => desp.RememberMe);

        config.NewConfig<UserEntity, UserForTableItemResponse>()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Avatar, desp => desp.Avatar.ImagePath);

        config.NewConfig<AvatarEntity, AvatarResponse>()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Name, desp => desp.ImagePath);

        config.NewConfig<CheckHasUserSentRequestToChangePasswordRequest, CheckHasUserSentRequestToChangePasswordQuery>()
            .Map(src => src.Key, desp => desp.Key);

        config.NewConfig<ChangePasswordRequest, ChangePasswordCommand>()
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.Password, desp => desp.Password)
            .Map(src => src.ConfirmPassword, desp => desp.ConfirmPassword);

        config.NewConfig<UserEntity, InformationAboutUserResponse>()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Email, desp => desp.Email)
            .Map(src => src.AvatarName, desp => desp.Avatar != null ? desp.Avatar.ImagePath : "");
    }
}
