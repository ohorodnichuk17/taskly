using Taskly_Application.Interfaces.IRepository;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain;

namespace Taskly_Infrastructure.Services;

public class RuleEvaluatorService(ITableItemsRepository tableItemsRepository,
    IFeedbackRepository feedbacksRepository, IInviteRepository inviteRepository) 
    : IRuleEvaluatorService
{
    public async Task<int> EvaluateRuleAsync(string ruleKey, Guid userId)
    {
        try
        {
            return ruleKey switch
            {
                Constants.RuleKey_CompletedTableItems =>
                    await tableItemsRepository.CountCompletedTasksAsync(userId),
            
                Constants.RuleKey_CountUserFeedbacks =>
                    await feedbacksRepository.CountUserFeedbacksAsync(userId),
                
                Constants.RuleKey_CountUserReferrals =>
                    await inviteRepository.CountSuccessfulInvitesAsync(userId),
            
                _ => 0 
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while evaluating the rule.", ex);
        }
    }
}