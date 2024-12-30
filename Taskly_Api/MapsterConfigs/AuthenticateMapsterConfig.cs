using Mapster;
using Taskly_Api.Request.Authenticate;
using Taskly_Application.Requests.Authentication.Command.SendVerificationEmail;
using Taskly_Application.Requests.Authentication.Command.VerificateEmail;

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
    }
}
