using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Taskly_Application.Requests.Authentication.Query.SendRequestToChangePassword;

public record SendRequestToChangePasswordQuery(string Email) : IRequest<ErrorOr<Guid>>;
