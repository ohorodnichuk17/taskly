namespace Taskly_Api.Response.Achievement;

public record AchievementResponse(Guid Id, 
    string Name, 
    string Description, 
    int Reward, 
    double PercentageOfCompletion, 
    string Icon, 
    bool IsCompleated);
