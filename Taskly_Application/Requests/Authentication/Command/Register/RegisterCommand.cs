using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Authentication.Command.Register;

public record RegisterCommand(string Email, string Password, string ConfirmPassword, Guid AvatarId, string ReferralCode) : IRequest<ErrorOr<string>>;
