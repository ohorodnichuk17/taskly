using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Authentication.Command.Register;

public record RegisterCommand(string Email, string Password, string ConfirmPassword, Guid AvatarId) : IRequest<ErrorOr<string>>;
