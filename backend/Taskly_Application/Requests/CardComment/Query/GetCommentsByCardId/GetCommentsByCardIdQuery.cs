using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.CardComment.Query.GetCommentsByCardId;

public record GetCommentsByCardIdQuery(Guid CardId) : IRequest<ErrorOr<CardCommentEntity[]>>;
