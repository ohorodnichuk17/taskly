using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Authentication.Query.CheckHasUserSentRequestToChangePassword;

public record CheckHasUserSentRequestToChangePasswordQuery(Guid Key) : IRequest<ErrorOr<string?>>;
