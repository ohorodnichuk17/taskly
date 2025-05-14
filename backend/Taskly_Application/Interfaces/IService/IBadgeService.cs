namespace Taskly_Application.Interfaces.IService;

public interface IBadgeService
{
    Task CheckAndAssignBadgeAsync(Guid userId);
}