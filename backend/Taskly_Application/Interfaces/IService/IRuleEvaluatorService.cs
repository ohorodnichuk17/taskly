namespace Taskly_Application.Interfaces.IService;

public interface IRuleEvaluatorService
{
    Task<int> EvaluateRuleAsync(string ruleKey, Guid userId);
}