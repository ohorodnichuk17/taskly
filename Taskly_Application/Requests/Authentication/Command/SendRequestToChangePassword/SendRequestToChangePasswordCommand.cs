using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Taskly_Application.Requests.Authentication.Command.SendRequestToChangePassword;

public record SendRequestToChangePasswordCommand(string Email) : IRequest<ErrorOr<Guid>>;
