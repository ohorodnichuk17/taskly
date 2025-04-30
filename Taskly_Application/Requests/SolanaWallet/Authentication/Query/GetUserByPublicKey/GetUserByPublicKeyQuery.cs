using MediatR;
using Taskly_Domain.Entities;
using ErrorOr;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Query.GetUserByPublicKey;

public record GetUserByPublicKeyQuery(
    string PublicKey) : IRequest<ErrorOr<UserEntity>>;