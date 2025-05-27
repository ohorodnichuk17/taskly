using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Authentication.Command.SendRequestToChangePassword;

public record SendRequestToChangePasswordCommand(string Email) : IRequest<ErrorOr<Guid>>;
