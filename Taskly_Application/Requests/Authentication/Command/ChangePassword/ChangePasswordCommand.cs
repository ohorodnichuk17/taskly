using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Authentication.Command.ChangePassword;

public record ChangePasswordCommand(string Email, string Password, string ConfirmPassword) : IRequest<ErrorOr<Guid>>;
