using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Authentication.Command.SendVerificationEmail;

public record SendVerificationCodeCommand(string Email) : IRequest<ErrorOr<string>>;
