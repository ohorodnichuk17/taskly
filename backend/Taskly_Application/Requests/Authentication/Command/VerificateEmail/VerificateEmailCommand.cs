using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Authentication.Command.VerificateEmail;

public record VerificateEmailCommand(string Email, string Code) : IRequest<ErrorOr<string>>;
