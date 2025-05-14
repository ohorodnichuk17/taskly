using MediatR;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query.GetRoleByUserId;

public record GetRoleByUserIdQuery(Guid UserId): IRequest<string>;
