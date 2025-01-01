using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Authentication.Query.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<string>>;
