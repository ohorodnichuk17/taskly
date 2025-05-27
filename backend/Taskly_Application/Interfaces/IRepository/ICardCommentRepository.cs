using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface ICardCommentRepository : IRepository<CardCommentEntity>
{
    Task<Guid?> LeaveCommentAsync(Guid CardId, Guid UserId, string Text);
    Task<CardCommentEntity[]?> GetCommentsByCardIdAsync(Guid CardId);
}
